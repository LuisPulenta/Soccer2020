using Prism.Navigation;

namespace Soccer2020.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        public ModifyUserPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Modificar Usuario";
        }
    }
}