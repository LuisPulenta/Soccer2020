using Prism.Navigation;
using Soccer2020.Common.Models;

namespace Soccer2020.Prism.ViewModels
{
    public class PredictionsTabbedPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;

        public PredictionsTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Predicciones para...";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("tournament"))
            {
                _tournament = parameters.GetValue<TournamentResponse>("tournament");
                Title = $"{_tournament.Name}";
            }
        }
    }
}