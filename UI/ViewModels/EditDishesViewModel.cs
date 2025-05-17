using System.Collections.ObjectModel;
using MenuManager.DB.Models;
using System.Windows.Input;
using Lab4.Commands;
using Lab4.Views;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Models;

namespace Lab4.ViewModels
{
    public class EditDishesViewModel : BaseViewModel
    {

        private readonly IDishService _dishService;
        public ObservableCollection<BaseMenuItemBusinessModel> dishes { get; set; }
        private BaseMenuItemBusinessModel _selectedDish;
        public BaseMenuItemBusinessModel SelectedDish
        {
            get
            {
                return _selectedDish;
            }
            set
            {
                _selectedDish = value;
                OnPropertyChanged(nameof(SelectedDish));
            }
        }
        public ICommand SaveChangesCommand { get; }
        public ICommand AddDishCommand { get; }
        public ICommand CreateComplexDishCommand { get; }
        public ICommand DeleteDishCommand { get; }
        public EditDishesViewModel(IDishService service)
        {
            _dishService = service;
            UpdateDishes();
            SaveChangesCommand = new RelayCommand(SaveChangesExecute, CanSaveChangesExecute);
            AddDishCommand = new RelayCommand(AddDishExecute, CanSaveChangesExecute);
            CreateComplexDishCommand = new RelayCommand(CreateComplexDishExecute, CanSaveChangesExecute);
            DeleteDishCommand = new RelayCommand(DeleteDishExecute, CanDeleteDishExecute);

        }
        private void SaveChangesExecute(object obj)
        {
            try
            {
                int.Parse(SelectedDish.Price.ToString());
                _dishService.UpdateDish(SelectedDish);
            }
            catch { }
            
        }
        private bool CanSaveChangesExecute(object obj)
        {

            return true;
        }
        private void AddDishExecute(object obj)
        {
            BaseMenuItemBusinessModel dish = new DishBusinessModel() { Category = DishCategory.Main, Description = "Опис страви", Name = "Назва Страви", Price = 999 };
            var createdDishID = _dishService.CreateDish(dish);
            UpdateDishes();
            OnPropertyChanged(nameof(dishes));
            SelectedDish = dishes.First(d => d.ID == createdDishID);

        }
        private void CreateComplexDishExecute(object obj)
        {
            var window = new CreateComplexMenuView(_dishService);
            window.Show();
            window.Focus();
        }
        private void UpdateDishes()
        {
            dishes = new(_dishService.GetAllDishesAndComplexDishes());
            OnPropertyChanged(nameof(dishes));
        }
        private void DeleteDishExecute(object obj)
        {
            _dishService.DeleteDish(SelectedDish.ID);
            UpdateDishes();
            OnPropertyChanged(nameof(dishes));
        }
        private bool CanDeleteDishExecute(object obj)
        {
            if (SelectedDish != null)
            {
                return true;
            }
            else return false;
        }
       


    }
}
