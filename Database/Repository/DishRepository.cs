using System.Linq.Expressions;
using AutoMapper;
using Database.Models;
using Database.Models.Database;
using Database.Services;
using Microsoft.Extensions.Logging;

namespace Database.Repository;

public class DishRepository : IDishRepository
{
    private readonly IDishQueryExecutorService _dishQueryExecutor;
    private readonly ILogger<DishRepository> _logger;
    private readonly IMapper _mapper;

    public DishRepository(IDishQueryExecutorService dishQueryExecutor, ILogger<DishRepository> logger, IMapper mapper)
    {
        _dishQueryExecutor = dishQueryExecutor;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishModel>> GetDishes(Expression<Func<Dish, bool>> query)
    {
        LogRequest(_logger, "GetDishes", query);
        var queryResult = await _dishQueryExecutor.ExecuteQuery(query);
        return queryResult.Select(x => _mapper.Map<DishModel>(x));
    }

    public async Task<IEnumerable<string>> GetUniqueMainCategories()
    {
        LogRequest(_logger, "GetUniqueMainCategories");
        return await _dishQueryExecutor.GetUniqueColumnValues(DishColumns.MainCategory);
    }

    public async Task<IEnumerable<string>> GetUniquePreparationTime()
    {
        LogRequest(_logger, "GetUniquePreparationTime");
        return await _dishQueryExecutor.GetUniqueColumnValues(DishColumns.MakeTimeMin);
    }

    public async Task<IEnumerable<string>> GetUniquePreparationDifficulty()
    {
        LogRequest(_logger, "GetUniquePreparationDifficulty");
        return await _dishQueryExecutor.GetUniqueColumnValues(DishColumns.PreparationDifficulty);
    }

    public async Task<IEnumerable<string>> GetUniqueIngredientsCategory()
    {
        LogRequest(_logger, "GetUniqueIngredientsCategory");
        return await _dishQueryExecutor.GetUniqueColumnValues(DishColumns.IngredientsCategory);
    }

    public async Task<IEnumerable<IEnumerable<string>>> GetAvailableIngredientsTags()
    {
        LogRequest(_logger, "GetAvailableIngredientsTags");
        var result = await _dishQueryExecutor.GetUniqueColumnValues(DishColumns.IngredientsTags);
        return result.Select(seq => seq.Trim().Split(',').Select(single => single.Trim()));
    }

    private static void LogRequest<T>(ILogger logger, string entry, T query)
    {
        logger.LogInformation
        (
            "New request was made to {DishRepositoryName}.Request entry {entry}. Request query {@Query}",
            nameof(DishRepository), entry, query
        );
    }

    private static void LogRequest(ILogger logger, string entry)
    {
        logger.LogInformation
        (
            "New request was made to {DishRepositoryName}.Request entry {entry}.",
            nameof(DishRepository), entry
        );
    }
}