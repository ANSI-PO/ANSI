using System.Linq.Expressions;
using Database.Models.Database;
using LinqKit;

namespace Domain.Services;

internal class DishQueryBuilder : IDishQueryBuilder
{
    private Expression<Func<Dish, bool>>? _query;

    public IDishQueryBuilder BasedOn(Expression<Func<Dish, bool>> query)
    {
        _query = query;

        return this;
    }

    public IDishQueryBuilder AndWith(Expression<Func<Dish, bool>> add)
    {
        ThrowExceptionWhenQueryIsNull();
        _query = _query.And(add);

        return this;
    }

    public Expression<Func<Dish, bool>> BuildDishExpression()
    {
        ThrowExceptionWhenQueryIsNull();
        return _query!;
    }

    private void ThrowExceptionWhenQueryIsNull()
    {
        if (_query is null)
        {
            throw new ArgumentNullException(nameof(_query),"Query builder was not properly initialized");
        }
    }
}