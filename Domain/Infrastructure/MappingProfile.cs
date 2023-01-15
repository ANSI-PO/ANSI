using AutoMapper;
using Domain.Models;
namespace Domain.Infrastructure;

// this class is automatically used by scanning assemblies
public class MappingProfile: Profile 
{
    public MappingProfile()
    {
        CreateMap<Database.Models.DishModel, Domain.Models.DishModel>().ReverseMap();
        
    }
    
}