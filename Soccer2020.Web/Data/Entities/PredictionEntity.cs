﻿using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Data.Entities
{
    public class PredictionEntity
    {
        public int Id { get; set; }

        public MatchEntity Match { get; set; }

        public UserEntity User { get; set; }

        [Display(Name = "GL")]
        public int? GoalsLocal { get; set; }

        [Display(Name = "GV")]
        public int? GoalsVisitor { get; set; }

        public int? Points { get; set; }
    }
}