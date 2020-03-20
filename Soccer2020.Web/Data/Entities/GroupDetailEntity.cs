using System.ComponentModel.DataAnnotations;

namespace Soccer2020.Web.Data.Entities
{
    public class GroupDetailEntity
    {
        public int Id { get; set; }

        public TeamEntity Team { get; set; }

        [Display(Name = "PJ")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "PG")]
        public int MatchesWon { get; set; }

        [Display(Name = "PE")]
        public int MatchesTied { get; set; }

        [Display(Name = "PP")]
        public int MatchesLost { get; set; }

        [Display(Name = "Pts")]
        public int? Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "GF")]
        public int GoalsFor { get; set; }

        [Display(Name = "GC")]
        public int GoalsAgainst { get; set; }

        [Display(Name = "DifG")]
        public int GoalDifference => GoalsFor - GoalsAgainst;

        public GroupEntity Group { get; set; }
    }
}