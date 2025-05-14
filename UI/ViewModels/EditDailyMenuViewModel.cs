using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab4.Commands;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.DailyMenusRepository;
using MenuManager.Repository.DishesRepository;

namespace Lab4.ViewModels
{
    public class EditDailyMenuViewModel : BaseViewModel
    {
        DishesRepository dishesRepository;
        DailyMenusRepository menusRepository;
        private List<BaseMenuItem> AllDishes;
        public List<BaseMenuItem> AllAvailableDishes { get; set; }
        public BaseMenuItem SelectedToAdd { get; set; }
        public BaseMenuItem SelectedToRemove { get; set; }
        private DailyMenu _originalMenu;
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
                SelectedDay = menusRepository.GetMenu().FirstOrDefault(x => x.DayOfWeek == value);
                AllDishes = new(dishesRepository.GetDishes().Concat(dishesRepository.GetComplexDishes()));
                AllAvailableDishes = AllDishes.Except(SelectedDay.Dishes).ToList();
                OnPropertyChanged(nameof(AllAvailableDishes));
                OnPropertyChanged(nameof(SelectedDay));
            }
        }
        public DailyMenu SelectedDay { get; set; }
        public EditDailyMenuViewModel(MenuContext context)
        {
            dishesRepository = new(context);
            menusRepository = new(context);
            DaysOfWeek = menusRepository.GetMenu().Select(x => x.DayOfWeek).ToList();
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
            AllAvailableDishes = AllDishes.Except(SelectedDay.Dishes).ToList();
            OnPropertyChanged(nameof(AllAvailableDishes));
            OnPropertyChanged(nameof(SelectedDay));
        }
        private void RemoveDishExecute(object obj)
        {
            if (SelectedToRemove != null)
            {
                SelectedDay.Dishes.Remove(SelectedToRemove);
            }
            AllAvailableDishes = AllDishes.Except(SelectedDay.Dishes).ToList();
            OnPropertyChanged(nameof(AllAvailableDishes));
            OnPropertyChanged(nameof(SelectedDay));

        }
        private void SaveChangesExecute(object obj)
        {
            if(SelectedDay != null)
            menusRepository.UpdateMenu(SelectedDay);
        }
    }
}
