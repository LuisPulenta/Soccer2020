using Newtonsoft.Json;
using Prism.Navigation;
using Soccer2020.Common.Helpers;
using Soccer2020.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soccer2020.Prism.ViewModels
{
    public class ClosedMatchesPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;
        private List<MatchResponse> _matches;

        public ClosedMatchesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Finalizados";
            LoadMatches();
        }

        public List<MatchResponse> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        private void LoadMatches()
        {
            _tournament = JsonConvert.DeserializeObject<TournamentResponse>(Settings.Tournament);
            List<MatchResponse> matches = new List<MatchResponse>();
            foreach (GroupResponse group in _tournament.Groups)
            {
                matches.AddRange(group.Matches);
            }

            Matches = matches.Where(m => m.IsClosed).OrderBy(m => m.Date).ToList();
        }
    }
}