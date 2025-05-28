using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.Profiles;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DomainData.UoW;
using Lab4.Commands;
using Lab4.Views;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.DailyMenusRepository;
using MenuManager.Repository.OrdersRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Lab4.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IDishService _dishService;
        private readonly IDailyMenuService _dailyMenuService;
        private readonly IOrderService _orderService;

        public OrderBusinessModel CurrentOrder { get; set; }
        public ObservableCollection<BaseMenuItemBusinessModel> OrderDishes { get; set; } = new();

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

        public IEnumerable<BaseMenuItemBusinessModel> FilteredDishes
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
        public MainViewModel(IDailyMenuService dailyMenuService, IOrderService orderService, IDishService dishService) {
            
            _dailyMenuService = dailyMenuService;
            _orderService = orderService;
            _dishService = dishService;

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

            CurrentOrder = new OrderBusinessModel();
        }

        public ObservableCollection<BaseMenuItemBusinessModel> TodayDishes { get; private set; } = new ObservableCollection<BaseMenuItemBusinessModel>();

        
        private void LoadMenuForDate(string day)
        {
            TodayDishes.Clear();

            var menu = _dailyMenuService.GetAllMenus(); 
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
            if(obj is BaseMenuItemBusinessModel dish)
            {
                CurrentOrder.AddDish(dish);
                OrderDishes.Add(dish);
                OnPropertyChanged(nameof(CurrentOrder));
  
            }
        }
        private bool CanAddToOrderExecute(object obj)
        {
            if (obj is BaseMenuItemBusinessModel dish)
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
            if(obj is BaseMenuItemBusinessModel dish)
            {
                CurrentOrder.RemoveDish(dish);
                OrderDishes.Remove(dish);
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
                _orderService.CreateOrder(CurrentOrder);
            }
            CurrentOrder = null;
            OrderDishes = null;
            CurrentOrder = new OrderBusinessModel();
            OnPropertyChanged(nameof(CurrentOrder));
            
        }
        private bool CanConfirmOrderExecute(object obj)
        {
            if (CurrentOrder.dishes.Count == 0) return false;
            else return true;
        }
        private void OpenDishesEditExecute(object obj)
        {
            var newWindow = new EditDishesView(_dishService);
            newWindow.Show();
            newWindow.Focus();
        }
        private void OpenOrdersEditExecute(object obj)
        {
            var newWindow = new EditOrdersView(_orderService);
            newWindow.Show();
            newWindow.Focus();
        }
        private void ResetCategoryFilterExecute(object obj)
        {
            SelectedCategory = null;
        }
        private void OpenDailyMenuEditExecute(object obj)
        {
            var newWindow = new EditDailyMenuView(_dishService, _dailyMenuService);
            newWindow.Show();
            newWindow.Focus();
        }
    }
}