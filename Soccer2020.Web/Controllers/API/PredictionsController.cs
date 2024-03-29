﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer2020.Common.Models;
using Soccer2020.Web.Data;
using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2020.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public PredictionsController(DataContext context, IConverterHelper converterHelper, IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("GetPredictionsForUser")]
        public async Task<IActionResult> GetPredictionsForUser([FromBody] PredictionsForUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TournamentEntity tournament = await _context.Tournaments.FindAsync(request.TournamentId);
            if (tournament == null)
            {
                return BadRequest("Este Torneo no existe.");
            }

            UserEntity userEntity = await _context.Users
                .Include(u => u.Team)
                .Include(u => u.Predictions)
                .ThenInclude(p => p.Match)
                .ThenInclude(m => m.Local)
                .Include(u => u.Predictions)
                .ThenInclude(p => p.Match)
                .ThenInclude(m => m.Visitor)
                .Include(u => u.Predictions)
                .ThenInclude(p => p.Match)
                .ThenInclude(p => p.Group)
                .ThenInclude(p => p.Tournament)
                .FirstOrDefaultAsync(u => u.Id == request.UserId.ToString());
            if (userEntity == null)
            {
                return BadRequest("Este Usuario no existe.");
            }

            // Add precitions already done
            List<PredictionResponse> predictionResponses = new List<PredictionResponse>();
            foreach (PredictionEntity predictionEntity in userEntity.Predictions)
            {
                if (predictionEntity.Match.Group.Tournament.Id == request.TournamentId)
                {
                    predictionResponses.Add(_converterHelper.ToPredictionResponse(predictionEntity));
                }
            }

            // Add precitions undone
            List<MatchEntity> matches = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Include(g=>g.Group)
                .Where(m => m.Group.Tournament.Id == request.TournamentId)
                .ToListAsync();
            foreach (MatchEntity matchEntity in matches)
            {
                PredictionResponse predictionResponse = predictionResponses.FirstOrDefault(pr => pr.Match.Id == matchEntity.Id);
                if (predictionResponse == null)
                {
                    predictionResponses.Add(new PredictionResponse
                    {
                        Match = _converterHelper.ToMatchResponse(matchEntity),
                    });
                }
            }

            return Ok(predictionResponses.OrderBy(pr => pr.Id).ThenBy(pr => pr.Match.Date));
        }

        [HttpPost]
        public async Task<IActionResult> PostPrediction([FromBody] PredictionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            MatchEntity matchEntity = await _context.Matches.FindAsync(request.MatchId);
            if (matchEntity == null)
            {
                return BadRequest("Este partido no existe.");
            }

            if (matchEntity.IsClosed)
            {
                return BadRequest("Este partido está cerrado.");
            }

            UserEntity userEntity = await _userHelper.GetUserAsync(request.UserId);
            if (userEntity == null)
            {
                return BadRequest("Este usuario no existe.");
            }

            if (matchEntity.Date <= DateTime.UtcNow)
            {
                return BadRequest("Este partido ya empezó.");
            }

            PredictionEntity predictionEntity = await _context.Predictions
                .FirstOrDefaultAsync(p => p.User.Id == request.UserId.ToString() && p.Match.Id == request.MatchId);

            if (predictionEntity == null)
            {
                predictionEntity = new PredictionEntity
                {
                    GoalsLocal = request.GoalsLocal,
                    GoalsVisitor = request.GoalsVisitor,
                    Match = matchEntity,
                    User = userEntity
                };

                _context.Predictions.Add(predictionEntity);
            }
            else
            {
                predictionEntity.GoalsLocal = request.GoalsLocal;
                predictionEntity.GoalsVisitor = request.GoalsVisitor;
                _context.Predictions.Update(predictionEntity);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionsByTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TournamentEntity tournament = await _context.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(g => g.Matches)
                .ThenInclude(m => m.Predictions)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.Team)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tournament == null)
            {
                return BadRequest("Este Torneo no existe");
            }

            List<PositionResponse> positionResponses = new List<PositionResponse>();
            foreach (GroupEntity groupEntity in tournament.Groups)
            {
                foreach (MatchEntity matchEntity in groupEntity.Matches)
                {
                    foreach (PredictionEntity predictionEntity in matchEntity.Predictions)
                    {
                        PositionResponse positionResponse = positionResponses.FirstOrDefault(pr => pr.UserResponse.Id == predictionEntity.User.Id);
                        if (positionResponse == null)
                        {
                            int? pp = 0;

                            if (predictionEntity.Points==null) {pp = 0;};
                            
                            if (!(predictionEntity.Points == null)) { pp = predictionEntity.Points; };
                            
                            positionResponses.Add(new PositionResponse
                            {
                                Points = pp,
                                UserResponse = _converterHelper.ToUserResponse(predictionEntity.User),
                            });
                        }
                        else
                        {
                            int? pp = 0;

                            if (predictionEntity.Points == null) { pp = 0; };

                            if (!(predictionEntity.Points == null)) { pp = predictionEntity.Points; };
                            
                            positionResponse.Points += pp;
                        }
                    }
                }
            }

            List<PositionResponse> list = positionResponses.OrderByDescending(pr => pr.Points).ToList();
            int i = 1;
            foreach (PositionResponse item in list)
            {
                item.Ranking = i;
                i++;
            }

            return Ok(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<UserEntity> users = await _context.Users
                .Include(u => u.Team)
                .Include(u => u.Predictions)
                .ToListAsync();
            List<PositionResponse> positionResponses = users.Select(u => new PositionResponse
            {
                Points = u.Points,
                UserResponse = _converterHelper.ToUserResponse(u)

            }).ToList();

            List<PositionResponse> list = positionResponses.OrderByDescending(pr => pr.Points).ToList();
            int i = 1;
            foreach (var item in list)
            {
                item.Ranking = i;
                i++;
            }

            return Ok(list);
        }
    }
}