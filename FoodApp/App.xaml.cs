using FoodApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Database.Infrastructure;
using Domain.Infrastructure;
using Domain.Services;
using System.Security.Cryptography.X509Certificates;
using FrontEnd.Infrastructures;

namespace FoodApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
       
        private void ConfigureServices(IServiceCollection services)
        {

           services
                    .SetupDomainDi();

            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddScoped<IDishSelectorService, DishSelectorService>();
            services.AddSingleton<MainView>();
            services.AddTransient<QuestionView>();
            services.AddTransient<ResponseView>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainView>();
            mainWindow.Show();
        }
    }
}
