using Domain.Abstractions;
using DishModel = Domain.Models.DishModel;

namespace Domain.Services;

public class DishSelectorService : IDishSelectorService
{
    private readonly IDishDatabase _database;
    private readonly IDishQueryBuilder _queryBuilder;

    public DishSelectorService(IDishDatabase database, IDishQueryBuilder queryBuilder)
    {
        _database = database;
        _queryBuilder = queryBuilder;
    }

    public async Task<IEnumerable<DishModel>> SelectDishes()
    {
        
        
        //example request
        var uniquePrepTime = await _database.GetUniquePreparationTime();
        // create expression via builder pattern 
        var expression = _queryBuilder
            .BasedOn(x => x.MakeTimeMin == int.Parse(uniquePrepTime.First()))
            .BuildDishExpression();


        //we receive some list of Dishes  
        var results = await _database.SelectDishes(expression);


        return results;
    }
}