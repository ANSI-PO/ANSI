using AutoMapper;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Infrastructures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.DishModel?, FrontEnd.Models.DishModel?>().ReverseMap();
            CreateMap<Domain.Models.AnswerModel, FrontEnd.Models.AnswerModel>().ReverseMap();
            CreateMap<Domain.Models.QuestionModel, FrontEnd.Models.QuestionModel>().ReverseMap();
        }

    }
}
