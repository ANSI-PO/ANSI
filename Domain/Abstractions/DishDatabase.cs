using System.Linq.Expressions;
using AutoMapper;
using Database.Models.Database;
using Database.Repository;
using Domain.Models;

namespace Domain.Abstractions;

internal class DishDatabase : IDishDatabase
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public DishDatabase(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishModel>> SelectDishes(Expression<Func<Dish, bool>> query)
    {
        var queryResult = await _dishRepository.GetDishes(query);
        return queryResult.Select(x=> _mapper.Map<DishModel>(x));
    }
}