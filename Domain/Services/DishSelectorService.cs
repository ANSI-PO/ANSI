using Database.Models;
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
        var ingredientsToLookFor = new[] {IngredientsCategoryTypes.Meat, IngredientsCategoryTypes.Fish};

        // create expression via builder pattern 
        var expression = _queryBuilder
            .BasedOn(x => x.MakeTimeMin > 10)
            .AndWith(x => ingredientsToLookFor.Contains(x.IngredientsCategory))
            .AndWith(x => x.MakeTimeMin < 120)
            .AndWith(x => x.PreparationDifficulty == PreparationDifficultyTypes.Easy)
            .AndWith(x => x.MainCategory == MainCategoryTypes.American)
            .BuildDishExpression();


        //we receive some list of Dishes  
        var results = await _database.SelectDishes(expression);


        return results;
    }
}