using System.Linq.Expressions;
using Database.Models;
using Database.Models.Database;

namespace Database.Repository;

public interface IDishRepository
{
    Task<IEnumerable<DishModel>> GetDishes(Expression<Func<Dish, bool>> query);
    Task<IEnumerable<string>> GetUniqueMainCategories();
    Task<IEnumerable<string>> GetUniquePreparationTime();
    Task<IEnumerable<string>> GetUniquePreparationDifficulty();
    Task<IEnumerable<string>> GetUniqueIngredientsCategory();
    Task<IEnumerable<IEnumerable<string>>> GetAvailableIngredientsTags();


}