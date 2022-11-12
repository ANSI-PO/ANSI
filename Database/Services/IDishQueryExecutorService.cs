using System.Linq.Expressions;
using Database.Models.Database;

namespace Database.Services;

public interface IDishQueryExecutorService
{
    Task<IEnumerable<Dish>> ExecuteQuery(Expression<Func<Dish, bool>> query);
}