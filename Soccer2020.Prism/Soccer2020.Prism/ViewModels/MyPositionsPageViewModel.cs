using Prism.Navigation;

namespace Soccer2020.Prism.ViewModels
{
    public class MyPositionsPageViewModel : ViewModelBase
    {
        public MyPositionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Mis Posiciones";
        }
    }
}