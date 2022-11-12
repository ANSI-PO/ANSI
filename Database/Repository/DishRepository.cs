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
        _logger.LogInformation
        (
            "New request was made to {DishRepositoryName}. Request query {@Query}",
            nameof(DishRepository), query
        );
    
        var queryResult = await _dishQueryExecutor.ExecuteQuery(query);
        
        return queryResult.Select(x => _mapper.Map<DishModel>(x));
    }
}