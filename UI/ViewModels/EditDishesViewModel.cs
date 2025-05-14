using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using MenuManager.DB.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MenuManager.Repository.DishesRepository;
using System.Windows.Input;
using System.Printing;
using Lab4.Commands;
using Lab4.Views;
using System.Text.RegularExpressions;

namespace Lab4.ViewModels
{
    public class EditDishesViewModel : BaseViewModel
    {
        
        private MenuContext context;
        private DishesRepository dishesRepository;
        public ObservableCollection<BaseMenuItem> dishes { get; set; }
        private BaseMenuItem _selectedDish;
        public BaseMenuItem SelectedDish
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
        public EditDishesViewModel(MenuContext context)
        {
            this.context = context;
            dishesRepository = new DishesRepository(context);
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
                dishesRepository.UpdateDish(SelectedDish);
            }
            catch { }
            
        }
        private bool CanSaveChangesExecute(object obj)
        {

            return true;
        }
        private void AddDishExecute(object obj)
        {
            BaseMenuItem dish = new Dish() { Category = DishCategory.Main, Description = "Опис страви", Name = "Назва Страви", Price = 999 };
            dishesRepository.InsertDish(dish);
            
            
            UpdateDishes();
            OnPropertyChanged(nameof(dishes));
            SelectedDish = dish;

        }
        private void CreateComplexDishExecute(object obj)
        {
            var window = new CreateComplexMenuView(context);
            window.Show();
            window.Focus();
        }
        private void UpdateDishes()
        {
            dishes = new(dishesRepository.GetDishes().Concat(dishesRepository.GetComplexDishes()));
        }
        private void DeleteDishExecute(object obj)
        {
            dishesRepository.DeleteDish(SelectedDish.ID);
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
