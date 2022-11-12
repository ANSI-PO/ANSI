
using Domain.Models;

namespace Domain.Services;

public interface IDishSelectorService
{
    Task<IEnumerable<DishModel>> SelectDishes();
}