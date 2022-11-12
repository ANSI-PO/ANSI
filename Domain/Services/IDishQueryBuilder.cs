using System.Linq.Expressions;
using Database.Models.Database;

namespace Domain.Services;

public interface IDishQueryBuilder
{
    IDishQueryBuilder BasedOn(Expression<Func<Dish, bool>> query);
    IDishQueryBuilder AndWith(Expression<Func<Dish, bool>> add);
    Expression<Func<Dish, bool>> BuildDishExpression();
}