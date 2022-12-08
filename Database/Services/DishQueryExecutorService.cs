using System.Collections;
using System.Linq.Expressions;
using Database.Infrastructure;
using Database.Infrastructure.Exceptions;
using Database.Models;
using Database.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database.Services;

internal class DishQueryExecutorService : IDishQueryExecutorService
{
    private readonly ANSIContext _context;
    private readonly ILogger<DishQueryExecutorService> _logger;

    public DishQueryExecutorService(ANSIContext context, ILogger<DishQueryExecutorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Dish>> ExecuteQuery(Expression<Func<Dish, bool>> query)
    {
        try
        {
            return await GetQueryableDish().Where(query).ToListAsync();
        }
        catch (Exception e)
        {
            const string message = "Query was not able to execute. Please check your query and inner error message";
            _logger.LogError("Critical error {Message}. For query: {@Query}", message, query);

            throw new DatabaseQueryExecutionException(message, e);
        }
    }

    public async Task<IEnumerable<string>> GetUniqueColumnValues(DishColumns dishColumn)
    {
        try
        {
            return dishColumn switch
            {
                DishColumns.DishName => await GetDistinct(x => x.DishName),
                DishColumns.IngredientsCategory => await GetDistinct(x => x.IngredientsCategory),
                DishColumns.IngredientsTags => await GetDistinct(x => x.IngredientsTags),
                DishColumns.MainCategory => await GetDistinct(x => x.MainCategory),
                DishColumns.MakeTimeMin => await GetDistinct(x => x.MakeTimeMin, i => i.ToString()),
                DishColumns.PreparationDifficulty => await GetDistinct(x => x.PreparationDifficulty),
                _ => throw new ArgumentOutOfRangeException
                (
                    nameof(dishColumn),
                    $"Not implemented statement for value {dishColumn}"
                )
            };
        }
        catch (Exception e)
        {
            const string message =
                "Distinct query was not able to execute. Please check your query and inner error message";
            _logger.LogError("Critical error {Message}. For Distinct column {Column}", message, dishColumn);
            throw new DatabaseQueryExecutionException(message, e);
        }
    }

    private IQueryable<Dish> GetQueryableDish() =>
        _context.Dishes.AsQueryable();

    private Task<List<T>> GetDistinct<T>(Expression<Func<Dish, T>> selector) =>
        GetQueryableDish().Select(selector).Distinct().ToListAsync();

    private async Task<IEnumerable<TO>> GetDistinct<TI, TO>(Expression<Func<Dish, TI>> selector, Func<TI, TO> mapper)
    {
        var dbResponse = await GetQueryableDish().Select(selector).Distinct().ToListAsync();
        return dbResponse.Select(mapper).ToList();
    }
}