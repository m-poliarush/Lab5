using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Commands;
using MenuManager.DB.Models;
using System.Windows.Input;
using System.ComponentModel.Design;
using MenuManager.DB;
using MenuManager.Repository.DishesRepository;
using System.Windows.Navigation;

namespace Lab4.ViewModels
{
    public class CreateComplexMenuViewModel : BaseViewModel
    {
        private readonly List<BaseMenuItem> _allDishes;
        private DishesRepository dishesRepository;

        public ComplexDish ComplexDish { get; } = new() {Name= "Name of Complex Mneu", Category= DishCategory.Complex, Description="Desc of complex menu", Price=999 };
        public List<DishSelection> AvailableDishes { get; }

        public ICommand SaveCommand { get; }

        public CreateComplexMenuViewModel(MenuContext context) {
            dishesRepository = new(context);
            _allDishes = dishesRepository.GetDishes().ToList();
            AvailableDishes = _allDishes.Select(d => new DishSelection(d)).ToList();
            SaveCommand = new RelayCommand(SaveExecute, (object obj) => true);

        }
        private void SaveExecute(object obj)
        {
            List<BaseMenuItem> list = AvailableDishes.Where(x => x.IsSelected == true).Select(x => x.Dish).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item is Dish dish)
                        ComplexDish.AddDish(dish);
                }
                dishesRepository.InsertDish(ComplexDish);
            }
        }
    }
    public class DishSelection
    {
        public BaseMenuItem Dish { get; }
        public bool IsSelected { get; set; }

        public DishSelection(BaseMenuItem dish)
        {
            Dish = dish;
        }
    }
}
