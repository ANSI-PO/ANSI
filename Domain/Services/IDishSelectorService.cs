
using Domain.Models;

namespace Domain.Services;

public interface IDishSelectorService
{
    Task<IEnumerable<DishModel>> SelectDishes();
    Task<List<QuestionModel>> GetFirstQuestion();
    Task<List<QuestionModel>> GetSecondQuestion(List<QuestionModel> pytania);
    Task<List<QuestionModel>> GetThirdQuestion(List<QuestionModel> pytania);
    Task<List<QuestionModel>> GetFourthQuestion(List<QuestionModel> pytania);
    Task<List<QuestionModel>> GetFifthQuestion(List<QuestionModel> pytania);
    Task<DishModel?> GetDish(List<QuestionModel> questions);
}