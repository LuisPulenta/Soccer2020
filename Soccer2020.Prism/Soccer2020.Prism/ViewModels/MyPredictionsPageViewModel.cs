using Prism.Navigation;

namespace Soccer2020.Prism.ViewModels
{
    public class MyPredictionsPageViewModel : ViewModelBase
    {
        public MyPredictionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Mis Predicciones";
        }
    }
}