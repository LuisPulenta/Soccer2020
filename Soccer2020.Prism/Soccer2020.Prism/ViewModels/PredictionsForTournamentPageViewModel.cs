using Newtonsoft.Json;
using Prism.Navigation;
using Soccer2020.Common.Helpers;
using Soccer2020.Common.Models;
using Soccer2020.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer2020.Prism.ViewModels
{
    public class PredictionsForTournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TournamentResponse _tournament;
        private bool _isRunning;
        private List<PredictionItemViewModel> _predictions;

        public PredictionsForTournamentPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = "Predicciones para...";
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<PredictionItemViewModel> Predictions
        {
            get => _predictions;
            set => SetProperty(ref _predictions, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            //Title = $"{_tournament.Name}";
            LoadPredictionsAsync();
        }

        private async void LoadPredictionsAsync()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Verifique su conexión a Internet",
                    "Aceptar");
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new PredictionsForUserRequest
            {
                TournamentId = _tournament.Id,
                UserId = new Guid(user.Id)
            };

            Response response = await _apiService.GetPredictionsForUserAsync(url, "api", "/Predictions/GetPredictionsForUser", request, "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            List<PredictionResponse> list = (List<PredictionResponse>)response.Result;
            Predictions = list.Select(p => new PredictionItemViewModel (_apiService)
            {
                GoalsLocal = p.GoalsLocal,
                GoalsVisitor = p.GoalsVisitor,
                Id = p.Id,
                Match = p.Match,
                Points = p.Points,
                User = p.User
            })
                .Where(p => !p.Match.IsClosed || p.Match.DateLocal > DateTime.Now)
                .OrderBy(p => p.Match.Date)
                .ToList();
        }
    }
}