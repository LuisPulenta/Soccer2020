using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Soccer2020.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTeams();
        IEnumerable<SelectListItem> GetComboTeams(int id);
    }
}