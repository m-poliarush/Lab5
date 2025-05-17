using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using Lab4.Commands;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.DailyMenusRepository;
using MenuManager.Repository.DishesRepository;

namespace Lab4.ViewModels
{
    public class EditDailyMenuViewModel : BaseViewModel
    {
        private readonly IDishService _dishService;
        private readonly IDailyMenuService _dailyMenuService;
        private List<BaseMenuItemBusinessModel> AllDishes;
        public List<BaseMenuItemBusinessModel> AllAvailableDishes { get; set; }
        public BaseMenuItemBusinessModel SelectedToAdd { get; set; }
        public BaseMenuItemBusinessModel SelectedToRemove { get; set; }
        public DailyMenuBusinessModel SelectedDay { get; set; }
        public IEnumerable<string> DaysOfWeek { get; set; }

        public ICommand AddDishesCommand { get; }
        public ICommand RemoveDishesCommand { get; }
        public ICommand SaveChangesCommand { get; }

        private string _SelectedDayName;
        public string SelectedDayName
        {
            get => _SelectedDayName;
            set
            {
                _SelectedDayName = value;
                SelectedDay = _dailyMenuService.GetAllMenus().FirstOrDefault(menu => menu.DayOfWeek == _SelectedDayName);
                AllDishes = new(_dishService.GetAllDishesAndComplexDishes());
                AllAvailableDishes = AllDishes.Where(d => SelectedDay.Dishes.All(sd => sd.ID != d.ID)).ToList();
                OnPropertyChanged(nameof(AllAvailableDishes));
                OnPropertyChanged(nameof(SelectedDay));
            }
        }
        
        public EditDailyMenuViewModel(IDailyMenuService dailyMenuService, IDishService dishService)
        {
            _dailyMenuService = dailyMenuService;
            _dishService = dishService;
            DaysOfWeek = _dailyMenuService.GetAllMenus().Select(x => x.DayOfWeek).ToList();
            AddDishesCommand = new RelayCommand(AddDishExecute, (object obj) => true);
            RemoveDishesCommand = new RelayCommand(RemoveDishExecute, (object obj) => true);
            SaveChangesCommand = new RelayCommand(SaveChangesExecute, (object obj) => true);
        }

        private void AddDishExecute(object obj)
        {
            if (SelectedToAdd != null)
            {
                SelectedDay.Dishes.Add(SelectedToAdd);
            }
            AllAvailableDishes = AllDishes.Where(d => SelectedDay.Dishes.All(sd => sd.ID != d.ID)).ToList();
            OnPropertyChanged(nameof(AllAvailableDishes));
            OnPropertyChanged(nameof(SelectedDay));
        }
        private void RemoveDishExecute(object obj)
        {
            if (SelectedToRemove != null)
            {
                SelectedDay.Dishes.Remove(SelectedToRemove);
            }
            AllAvailableDishes = AllDishes.Where(d => SelectedDay.Dishes.All(sd => sd.ID != d.ID)).ToList();
            OnPropertyChanged(nameof(AllAvailableDishes));
            OnPropertyChanged(nameof(SelectedDay));

        }
        private void SaveChangesExecute(object obj)
        {
            if (SelectedDay != null)
                _dailyMenuService.UpdateMenu(SelectedDay);
        }
    }
}
