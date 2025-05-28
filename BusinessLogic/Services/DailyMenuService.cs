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

            

            return result;
        }
        public void UpdateMenu(DailyMenuBusinessModel menu)
        {
            var originalDishlist = menu.Dishes;
            var menuEntity = _unitOfWork.MenusRepository.GetTrackedOrAttach(menu.DayID, m => m.Dishes);
            menuEntity.Dishes.Clear();
            _unitOfWork.MenusRepository.Update(menuEntity);
            _unitOfWork.Save();
            foreach (var dish in originalDishlist)
            {
                if(dish is DishBusinessModel)
                {
                    var dishEntity = _unitOfWork.DishRepository.GetTrackedOrAttach(dish.ID);
                    menuEntity.Dishes.Add(dishEntity);
                }
                else if(dish is ComplexDishBusinessModel cd)
                {
                    var complexDishEntity = _unitOfWork.ComplexDishRepository.GetTrackedOrAttach(cd.ID);
                    menuEntity.Dishes.Add(complexDishEntity);

                }

            }
            _unitOfWork.MenusRepository.Update(menuEntity);

            
            _unitOfWork.Save();
        }
        
    }
}
