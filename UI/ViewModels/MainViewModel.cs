using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using Lab4.Commands;
using Lab4.Views;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.DailyMenusRepository;
using MenuManager.Repository.OrdersRepository;
using Microsoft.EntityFrameworkCore;

namespace Lab4.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MenuContext context;
        private DailyMenusRepository menusRepository;
        private OrdersRepository ordersRepository;

        public Order CurrentOrder { get; set; }

        public ICommand SelectDayCommand { get; }
        public ICommand AddToOrderCommand { get; }
        public ICommand DeleteFromOrderCommand { get; }
        public ICommand ConfirmOrderCommand { get; }
        public ICommand OpenDishesEditCommand { get; }
        public ICommand OpenOrdersEditCommand { get; }
        public ICommand ResetCategoryFilterCommand { get; }
        public ICommand OpenDailyMenuEditCommand { get; }

        private DishCategory? _selectedCategory;
        public DishCategory? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredDishes));
            }
        }

        public IEnumerable<BaseMenuItem> FilteredDishes
        {
            get
            {
                if (SelectedCategory == null)
                {
                    return TodayDishes;
                }
                else
                {
                    return TodayDishes.Where(dish => dish.Category == SelectedCategory);
                }
            }
        }



        private string _selectedDay;
        public string SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                LoadMenuForDate(value);
                OnPropertyChanged(nameof(FilteredDishes));
                OnPropertyChanged(nameof(SelectedDay));
            }
        }
        public MainViewModel() {
            context = new MenuContext();
            menusRepository = new DailyMenusRepository(context);
            ordersRepository = new OrdersRepository(context);
            CultureInfo ua = new CultureInfo("uk-UA");
            string s = DateTime.Today.ToString("dddd", ua);
            LoadMenuForDate(s);            
            SelectDayCommand = new RelayCommand(SelectDayCommandExecute, CanSelectDauCommandExecute);
            AddToOrderCommand = new RelayCommand(AddToOrderExecute, CanAddToOrderExecute);
            DeleteFromOrderCommand = new RelayCommand(DeleteFromOrderExecute, CanDeleteFromOrderExecute);
            ConfirmOrderCommand = new RelayCommand(ConfirmOrderExecute, CanConfirmOrderExecute);
            OpenDishesEditCommand = new RelayCommand(OpenDishesEditExecute, CanDeleteFromOrderExecute);
            OpenOrdersEditCommand = new RelayCommand(OpenOrdersEditExecute, CanDeleteFromOrderExecute);
            ResetCategoryFilterCommand = new RelayCommand(ResetCategoryFilterExecute, CanDeleteFromOrderExecute);
            OpenDailyMenuEditCommand = new RelayCommand(OpenDailyMenuEditExecute, (object obj) => true);

            CurrentOrder = new Order();
        }

        public ObservableCollection<BaseMenuItem> TodayDishes { get; private set; } = new ObservableCollection<BaseMenuItem>();

        
        private void LoadMenuForDate(string day)
        {
            TodayDishes.Clear();

            var menu = menusRepository.GetMenu();
            var dailyMenu = menu?.FirstOrDefault(x => x.DayOfWeek.ToLower() == day);

            if (dailyMenu?.Dishes != null)
            {
                foreach (var dish in dailyMenu.Dishes)
                {
                    TodayDishes.Add(dish);
                }
            }
        }


        private void AddToOrderExecute(object obj)
        {
            if(obj is BaseMenuItem dish)
            {
                CurrentOrder.AddDish(dish);
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }
        private bool CanAddToOrderExecute(object obj)
        {
            if (obj is BaseMenuItem dish)
            {
                if (CurrentOrder.dishes.Contains(dish))
                {
                    return false;
                }
                else return true;
            }
            return false;
        }
        private void SelectDayCommandExecute(object obj)
        {
            if (obj != null)
            {
                SelectedDay = obj.ToString();
              
            }
        }
        private bool CanSelectDauCommandExecute(object obj)
        {
            if (CurrentOrder.dishes.Count != 0)
            {
                return false;
            }
            else return true;
        }
        private void DeleteFromOrderExecute(object obj)
        {
            if(obj is BaseMenuItem dish)
            {
                CurrentOrder.RemoveDish(dish);
                OnPropertyChanged(nameof(CurrentOrder));
                
            }
        }
        private bool CanDeleteFromOrderExecute(object obj)
        {
            return true;
        }
        private void ConfirmOrderExecute(object obj)
        {
            if(CurrentOrder.dishes.Count != 0)
            {
                ordersRepository.InsertOrder(CurrentOrder);
            }
            CurrentOrder = null;
            CurrentOrder = new Order();
            OnPropertyChanged(nameof(CurrentOrder));
            
        }
        private bool CanConfirmOrderExecute(object obj)
        {
            if (CurrentOrder.dishes.Count == 0) return false;
            else return true;
        }
        private void OpenDishesEditExecute(object obj)
        {
            var newWindow = new EditDishesView(context);
            newWindow.Show();
            newWindow.Focus();
        }
        private void OpenOrdersEditExecute(object obj)
        {
            var newWindow = new EditOrdersView(context);
            newWindow.Show();
            newWindow.Focus();
        }
        private void ResetCategoryFilterExecute(object obj)
        {
            SelectedCategory = null;
        }
        private void OpenDailyMenuEditExecute(object obj)
        {
            var newWindow = new EditDailyMenuView(context);
            newWindow.Show();
            newWindow.Focus();
        }
    }
}