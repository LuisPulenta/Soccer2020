using Microsoft.AspNetCore.Mvc;
using Soccer2020.Web.Data;
using Soccer2020.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Soccer2020.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public IEnumerable<TeamEntity> GetTeams()
        {
            return _context.Teams.OrderBy(pt => pt.Name);
        }
    }
}