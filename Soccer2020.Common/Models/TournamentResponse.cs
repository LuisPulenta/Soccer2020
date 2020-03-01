using System;
using System.Collections.Generic;

namespace Soccer2020.Common.Models
{
    public class TournamentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public bool IsActive { get; set; }

        public string LogoPath { get; set; }

        public List<GroupResponse> Groups { get; set; }

        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
           ? "noimage"//null
           : $"http://keypress.serveftp.net:88/Soccer2020Api{LogoPath.Substring(1)}";
    }
}