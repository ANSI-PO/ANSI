using System.Collections.Immutable;
using Domain.Abstractions;
using Domain.Models;

namespace Domain.Services;

public class DishSelectorService : IDishSelectorService
{
    private readonly IDishDatabase _database;
    private readonly IDishQueryBuilder _queryBuilder;
    private readonly Random _random = new();

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
            .AndWith(x => x.IngredientsTags.Contains("Wołowina"))
            .BuildDishExpression();

        //we receive some list of Dishes  
        var results = await _database.SelectDishes(expression);

        return results;
    }

    public async Task<List<QuestionModel>> GetFirstQuestion()
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel>();
        List<QuestionModel> pytania = new List<QuestionModel>();
        QuestionModel quest = new QuestionModel();
        var kategorie = await _database.GetUniqueMainCategories();
        int loopCount = 1;
        quest.QuestionId = 1;
        quest.QuestionName = "Jaką kuchnię wybierasz? ";
        foreach (var item in kategorie)
        {
            AnswerModel answer = new AnswerModel();
            answer.AnswerId = loopCount;
            loopCount++;
            answer.AnswerName = item;
            odpowiedzi.Add(answer);
        }

        quest.Answers = odpowiedzi;
        pytania.Add(quest);

        return pytania;
    }

    public async Task<List<QuestionModel>> GetSecondQuestion(List<QuestionModel> pytania)
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel>();
        QuestionModel quest = new QuestionModel();
        var prepTimes = await _database.GetUniquePreparationTime();
        int loopCount = 1;
        quest.QuestionId = 2;
        quest.QuestionName = "Ile czasu masz na przygotowanie posiłku? ";
        foreach (var item in prepTimes)
        {
            AnswerModel answer = new AnswerModel();
            answer.AnswerId = loopCount;
            loopCount++;
            answer.AnswerName = item;
            answer.isPicked = false;
            odpowiedzi.Add(answer);
        }

        quest.Answers = odpowiedzi;
        pytania.Add(quest);

        return pytania;
    }

    public async Task<List<QuestionModel>> GetThirdQuestion(List<QuestionModel> pytania)
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel>();
        QuestionModel quest = new QuestionModel();
        var prepDiff = await _database.GetUniquePreparationDifficulty();
        int loopCount = 1;
        quest.QuestionId = 3;
        quest.QuestionName = "Wybierz trudność przygotowywanego posiłku: ";
        foreach (var item in prepDiff)
        {
            AnswerModel answer = new AnswerModel();
            answer.AnswerId = loopCount;
            loopCount++;
            answer.AnswerName = item;
            answer.isPicked = false;
            odpowiedzi.Add(answer);
        }

        quest.Answers = odpowiedzi;
        pytania.Add(quest);

        return pytania;
    }

    public async Task<List<QuestionModel>> GetFourthQuestion(List<QuestionModel> pytania)
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel>();
        QuestionModel quest = new QuestionModel();
        var uniqueIngredients = await _database.GetUniqueIngredientsCategory();
        int loopCount = 1;
        quest.QuestionId = 4;
        quest.QuestionName = "Wybierz podstawowy składnik do posiłku: ";
        foreach (var item in uniqueIngredients)
        {
            AnswerModel answer = new AnswerModel();
            answer.AnswerId = loopCount;
            loopCount++;
            answer.AnswerName = item;
            answer.isPicked = false;
            odpowiedzi.Add(answer);
        }

        quest.Answers = odpowiedzi;
        pytania.Add(quest);

        return pytania;
    }

    public async Task<List<QuestionModel>> GetFifthQuestion(List<QuestionModel> pytania)
    {
        List<AnswerModel> odpowiedzi = new List<AnswerModel>();
        QuestionModel quest = new QuestionModel();
        var uniqueIngredients = await _database.GetUniqueTags();
        int loopCount = 1;
        quest.QuestionId = 5;
        quest.QuestionName = "Wybierz kategorię składników które musi zawierać potrawa: ";
        foreach (var item in uniqueIngredients)
        {
            AnswerModel answer = new AnswerModel();
            answer.AnswerId = loopCount;
            loopCount++;
            //answer.AnswerName = item;
            answer.isPicked = false;
            odpowiedzi.Add(answer);
        }

        quest.Answers = odpowiedzi;
        pytania.Add(quest);

        return pytania;
    }

    public async Task<DishModel?> GetDish(List<QuestionModel> questions)
    {
        var mainCategoryToLookFor = new List<string>();
        var preparationTimeToLookFor = new List<int>();
        var preparationDifficultyToLookFor = new List<string>();
        var ingredientsCategoryToLookFor = new List<string>();
        var uniqueIngredientsToLookFor = new List<string>();
        if (questions.Count() == 5)
        {
            // Pytanie o kuchnie świata
            foreach (var answer in questions[0].Answers)
            {
                if (answer.isPicked == true)
                {
                    mainCategoryToLookFor.Add(answer.AnswerName);
                }
            }

            // Pytanie o czas na przygotowanie posiłku
            foreach (var answer in questions[1].Answers)
            {
                if (answer.isPicked == true)
                {
                    preparationTimeToLookFor.Add(int.Parse(answer.AnswerName));
                }
            }

            // Pytanie o trudność przygotowywanego posiłku
            foreach (var answer in questions[2].Answers)
            {
                if (answer.isPicked == true)
                {
                    preparationDifficultyToLookFor.Add(answer.AnswerName);
                }
            }

            // Pytanie o podstawowy składkik do posiłku
            foreach (var answer in questions[3].Answers)
            {
                if (answer.isPicked == true)
                {
                    ingredientsCategoryToLookFor.Add(answer.AnswerName);
                }
            }

            // Pytanie o kategorię składników
            foreach (var answer in questions[4].Answers)
            {
                if (answer.isPicked == true)
                {
                    uniqueIngredientsToLookFor.Add(answer.AnswerName);
                }
            }
        }

        var expression = _queryBuilder
            .BasedOn(x => mainCategoryToLookFor.Contains(x.MainCategory))
            .AndWith(x => preparationTimeToLookFor.Contains(x.MakeTimeMin))
            .AndWith(x => preparationDifficultyToLookFor.Contains(x.PreparationDifficulty))
            .AndWith(x => ingredientsCategoryToLookFor.Contains(x.IngredientsCategory));
        foreach (var ingredient in uniqueIngredientsToLookFor)
        {
            expression.AndWith(x => x.IngredientsTags.Contains(ingredient));
        }

        var results = (await _database.SelectDishes(expression.BuildDishExpression())).ToImmutableList();

        return results.Count == 0 ? null : results[_random.Next(0, results.Count)];
    }
}