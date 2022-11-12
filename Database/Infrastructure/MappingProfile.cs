using AutoMapper;
using Database.Models;
using Database.Models.Database;

namespace Database.Infrastructure;

// this class is automatically used by scanning assemblies
public class MappingProfile: Profile 
{
    public MappingProfile()
    {
        CreateMap<Dish, DishModel>().ReverseMap();
    }
    
}