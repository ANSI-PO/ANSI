using System.Linq.Expressions;
using Database.Models.Database;
using Domain.Models;

namespace Domain.Abstractions;

public interface IDishDatabase
{
    Task<IEnumerable<DishModel>> SelectDishes(Expression<Func<Dish, bool>> query);
    Task<IEnumerable<string>> GetUniqueMainCategories();
    Task<IEnumerable<string>> GetUniquePreparationTime();
    Task<IEnumerable<string>> GetUniquePreparationDifficulty();
    Task<IEnumerable<string>> GetUniqueIngredientsCategory();
    Task<IEnumerable<IEnumerable<string>>> GetAvailableIngredientsTags();
}