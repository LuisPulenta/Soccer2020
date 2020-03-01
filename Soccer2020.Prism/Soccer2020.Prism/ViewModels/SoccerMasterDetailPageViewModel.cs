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

        public SoccerMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

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
                    Title = "Login"
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