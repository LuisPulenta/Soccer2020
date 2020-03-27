using Newtonsoft.Json;
using Prism.Navigation;
using Soccer2020.Common.Helpers;
using Soccer2020.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soccer2020.Prism.ViewModels
{
    public class SoccerMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public SoccerMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "tournament",
                    PageName = "TournamentsPage",
                    Title = "Torneos"
                },
                new Menu
                {
                    Icon = "prediction",
                    PageName = "MyPredictionsPage",
                    Title = "Mis Predicciones"
                },
                new Menu
                {
                    Icon = "medal",
                    PageName = "MyPositionsPage",
                    Title = "Mis Posiciones"
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = "Modificar Usuario"
                },
                new Menu
                {
                    Icon = "login",
                    PageName = "LoginPage",
                    Title = Settings.IsLogin ? "Cerrar sesión" : "Iniciar Sesión"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}