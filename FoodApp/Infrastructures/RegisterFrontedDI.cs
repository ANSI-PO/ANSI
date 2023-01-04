using Domain.Infrastructure;
using FrontEnd.Infrastructures;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Infrastructures
{
 

        public static class DependencyInjectionContainerRegistration
        {

            public static IServiceCollection SetupFrontEndDi(this IServiceCollection service)
            {
                service
                    .SetupDomainDi();

                service
                    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                service
                    .AddSingleton<IDishSelectorService, DishSelectorService>();
                return service;
            }

        }

}

