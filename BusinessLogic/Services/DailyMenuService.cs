using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DomainData.UoW;
using MenuManager.DB.Models;

namespace BusinessLogic.Services
{
    public class DailyMenuService : IDailyMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DailyMenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<DailyMenuBusinessModel> GetAllMenus()
        {
            var menus = _unitOfWork.MenusRepository.GetAll(dm => dm.Dishes);

            var complexDishes = _unitOfWork.ComplexDishRepository.GetAll(cd => cd.DishList);
            
            var result = _mapper.Map<List<DailyMenuBusinessModel>>(menus);

            foreach (var menu in result)
            {
                foreach (var dish in menu.Dishes.OfType<ComplexDishBusinessModel>())
                {
                    var complexDish = complexDishes.FirstOrDefault(cd => cd.ID == dish.ID);
                    if (complexDish != null)
                    {
                        dish.DishList = _mapper.Map<List<DishBusinessModel>>(complexDish.DishList);
                    }
                }
            }

            return result;
        }
        public void UpdateMenu(DailyMenuBusinessModel menu)
        {
            var menuEntity = _mapper.Map<DailyMenu>(menu);

            _unitOfWork.MenusRepository.Update(menuEntity);
            _unitOfWork.Save();
        }
        
    }
}
