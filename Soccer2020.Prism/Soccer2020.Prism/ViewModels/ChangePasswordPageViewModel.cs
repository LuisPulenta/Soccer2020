using Soccer2020.Common.Services;
using Prism.Commands;
using Prism.Navigation;
using Soccer2020.Common.Models;
using Newtonsoft.Json;
using Soccer2020.Common.Helpers;

namespace Soccer2020.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _changePasswordCommand;

        public ChangePasswordPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = "Cambio de Password";
        }

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void ChangePasswordAsync()
        {
            //var isValid = await ValidateDataAsync();
            //if (!isValid)
            //{
            //    return;
            //}

            IsRunning = true;
            IsEnabled = false;

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ChangePasswordRequest request = new ChangePasswordRequest
            {
                Email = user.Email,
                NewPassword = NewPassword,
                OldPassword = CurrentPassword,
            };

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.ChangePasswordAsync(url, "api", "/Account/ChangePassword", request, "bearer", token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                "Aceptar");
            await _navigationService.GoBackAsync();
        }

    }
}