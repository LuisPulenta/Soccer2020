using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer2020.Web.Data;
using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer2020.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TournamentsController(DataContext context,
                                     IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTournament()
        {
            List<TournamentEntity> tournaments = await _context.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
      //Esta parte no es necesaria si es que no quiero traer las predicciones
                //.Include(t => t.Groups)
                //.ThenInclude(g => g.Matches)
                //.ThenInclude(m => m.Predictions)
                //.ThenInclude(p => p.User)
                .Include(t => t.Groups)
                .ThenInclude(g => g.Matches)
                .ThenInclude(m => m.Local)
                .Include(t => t.Groups)
                .ThenInclude(g => g.Matches)
                .ThenInclude(m => m.Visitor)
                .ToListAsync();
            return Ok(_converterHelper.ToTournamentResponse(tournaments));
        }

    }
}