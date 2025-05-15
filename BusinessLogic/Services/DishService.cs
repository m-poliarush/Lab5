using AutoMapper;
using BusinessLogic.Models;
using DomainData.UoW;
using MenuManager.DB.Models;

namespace BusinessLogic.Services
{
    public class DishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DishService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<BaseMenuItemBusinessModel> GetAllDishes()
        {
            List<BaseMenuItemBusinessModel> result = new List<BaseMenuItemBusinessModel>();

            var dishes = _unitOfWork.DishRepository.GetAll();
            var complexDishes = _unitOfWork.ComplexDishRepository.GetAll(d => d.DishList);

            foreach (var dish in dishes)
            {
                result.Add(_mapper.Map<DishBusinessModel>(dish));
            }

            foreach (var complexDish in complexDishes)
            {
                result.Add(_mapper.Map<ComplexDishBusinessModel>(complexDish));
            }

            return result;
        }
        public void CreateDish(BaseMenuItemBusinessModel dish)
        {
            var baseMenuItemEntity = _mapper.Map<BaseMenuItem>(dish);

            if(baseMenuItemEntity is Dish d)
            {
                _unitOfWork.DishRepository.Create(d);
            }
            else if(baseMenuItemEntity is ComplexDish cd)
            {
                _unitOfWork.ComplexDishRepository.Create(cd);
            }
        }
        public void DeleteDish(int id)
        {
            BaseMenuItem dish = _unitOfWork.DishRepository.GetById(id);

            if(dish == null)
            {
                dish = _unitOfWork.ComplexDishRepository.GetById(id);
            }

            if(dish is ComplexDish)
            {
                _unitOfWork.ComplexDishRepository.Delete(id);
            }

            else if(dish is Dish)
            {
                _unitOfWork.DishRepository.Delete(id);
            }
            
        }
        public void UpdateDish(BaseMenuItemBusinessModel dish)
        {
            if(dish is DishBusinessModel)
            {
                var dishEntity = _mapper.Map<Dish>(dish);
                _unitOfWork.DishRepository.Update(dishEntity);
            }
            else if(dish is ComplexDishBusinessModel)
            {
                var complexDishEntity = _mapper.Map<ComplexDish>(dish);
                _unitOfWork.ComplexDishRepository.Update(complexDishEntity);
            }
            _unitOfWork.Save();
        }
    }
}
