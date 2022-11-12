using System.Linq.Expressions;
using Database.Models;
using Database.Models.Database;

namespace Database.Repository;

public interface IDishRepository
{
    Task<IEnumerable<DishModel>> GetDishes(Expression<Func<Dish, bool>> query);
}