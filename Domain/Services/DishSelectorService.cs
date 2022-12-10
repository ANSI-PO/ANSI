using Domain.Abstractions;
using Domain.Models;
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

    public Task<List<QuestionModel>> GetFirstQuestion ()
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel> ();
        List<QuestionModel> pytania = new List<QuestionModel> ();
        QuestionModel quest = new QuestionModel();
        var kategorie = MainCategoryTypes.Greek

        quest.QuestionId = 1;
        quest.QuestionName = "Jaką kuchnie wybierasz? ";
        
        

        pytania.Add();

        return ;
    }
}