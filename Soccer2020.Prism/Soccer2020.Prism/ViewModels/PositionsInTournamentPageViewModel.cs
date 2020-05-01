using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Soccer2020.Common.Helpers;
using Soccer2020.Common.Models;
using Soccer2020.Common.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soccer2020.Prism.ViewModels
{
    public class PositionsInTournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TournamentResponse _tournament;
        private bool _isRunning;
        private ObservableCollection<PositionResponse> _positions;
        private List<PositionResponse> _myPositions;
        private string _search;
        private DelegateCommand _searchCommand;

        public PositionsInTournamentPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = "Posiciones";
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowPositions));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowPositions();
            }
        }

        public ObservableCollection<PositionResponse> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            LoadPositionsAsync();
        }

        private async void LoadPositionsAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Revise su conexión a Internet",
                    "Aceptar");
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.GetListAsync<PositionResponse>(url, "api", $"/Predictions/{_tournament.Id}", "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            _myPositions = (List<PositionResponse>)response.Result;
            ShowPositions();
        }

        private void ShowPositions()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Positions = new ObservableCollection<PositionResponse>(_myPositions);
            }
            else
            {
                Positions = new ObservableCollection<PositionResponse>(
                    _myPositions.Where(p => p.UserResponse.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                                            p.UserResponse.LastName.ToUpper().Contains(Search.ToUpper())));
            }
        }
    }
}