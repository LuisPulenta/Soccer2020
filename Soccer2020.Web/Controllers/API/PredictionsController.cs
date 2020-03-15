﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer2020.Common.Models;
using Soccer2020.Web.Data;
using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer2020.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public PredictionsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetPredictionsForUser")]
        public async Task<IActionResult> GetPredictionsForUser([FromBody] PredictionRequest predictionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TournamentEntity tournament = await _context.Tournaments.FindAsync(predictionRequest.TournamentId);
            if (tournament == null)
            {
                return BadRequest("El torneo no existe");
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
                .FirstOrDefaultAsync(u => u.Id == predictionRequest.UserId.ToString());
            if (userEntity == null)
            {
                return BadRequest("El usuario no existe");
            }

            // Add precitions already done
            List<PredictionResponse> predictionResponses = new List<PredictionResponse>();
            foreach (PredictionEntity predictionEntity in userEntity.Predictions)
            {
                if (predictionEntity.Match.Group.Tournament.Id == predictionRequest.TournamentId)
                {
                    predictionResponses.Add(_converterHelper.ToPredictionResponse(predictionEntity));
                }
            }

            // Add precitions undone
            List<MatchEntity> matches = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Where(m => m.Group.Tournament.Id == predictionRequest.TournamentId)
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
    }
}