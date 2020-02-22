using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soccer2020.Web.Data;
using System.Collections.Generic;
using System.Linq;

namespace Soccer2020.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboTeams()
        {
            List<SelectListItem> list = _context.Teams.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elija un Equipo...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTeams(int id)
        {
            var list = _context.GroupDetails
                .Include(gd => gd.Team)
                .Where(gd => gd.Group.Id == id)
                .Select(gd => new SelectListItem
                {
                    Text = gd.Team.Name,
                    Value = $"{gd.Team.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Elija un Equipo...]",
                Value = "0"
            });

            return list;
        }

    }
}