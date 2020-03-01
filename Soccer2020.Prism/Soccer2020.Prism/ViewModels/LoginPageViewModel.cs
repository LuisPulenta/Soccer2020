using Prism.Navigation;

namespace Soccer2020.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
        }
    }
}