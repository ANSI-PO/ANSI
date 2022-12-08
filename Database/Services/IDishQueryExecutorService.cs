using System.Linq.Expressions;
using Database.Models;
using Database.Models.Database;

namespace Database.Services;

public interface IDishQueryExecutorService
{
    Task<IEnumerable<Dish>> ExecuteQuery(Expression<Func<Dish, bool>> query);

    Task<IEnumerable<string>> GetUniqueColumnValues(DishColumns dishColumn);
}