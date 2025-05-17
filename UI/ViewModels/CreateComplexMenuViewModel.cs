using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Commands;
using MenuManager.DB.Models;
using System.Windows.Input;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Models;

namespace Lab4.ViewModels
{
    public class CreateComplexMenuViewModel : BaseViewModel
    {
        private readonly List<BaseMenuItemBusinessModel> _allDishes;
        private readonly IDishService _dishService;

        public ComplexDishBusinessModel ComplexDish { get; } = new() {Name= "Name of Complex Mneu", Category= DishCategory.Complex, Description="Desc of complex menu", Price=999 };
        public List<DishSelection> AvailableDishes { get; }

        public ICommand SaveCommand { get; }

        public CreateComplexMenuViewModel(IDishService service) {
            _dishService = service;
            _allDishes = _dishService.GetAllDishes();
            AvailableDishes = _allDishes.Select(d => new DishSelection(d)).ToList();
            SaveCommand = new RelayCommand(SaveExecute, (object obj) => true);

        }
        private void SaveExecute(object obj)
        {
            List<BaseMenuItemBusinessModel> list = AvailableDishes.Where(x => x.IsSelected == true).Select(x => x.Dish).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item is DishBusinessModel dish)
                        ComplexDish.AddDish(dish);
                }
                _dishService.CreateDish(ComplexDish);
            }
        }
    }
    public class DishSelection
    {
        public BaseMenuItemBusinessModel Dish { get; }
        public bool IsSelected { get; set; }

        public DishSelection(BaseMenuItemBusinessModel dish)
        {
            Dish = dish;
        }
    }
}
