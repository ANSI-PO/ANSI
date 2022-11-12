using System.Linq.Expressions;
using Database.Infrastructure;
using Database.Infrastructure.Exceptions;
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

    private IQueryable<Dish> GetQueryableDish() =>
        _context.Dishes.AsQueryable();
}