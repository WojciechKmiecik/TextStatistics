using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TextStatistics.Dal;
using TextStatistics.Definition.Services;
using TextStatistics.Logic.Services;

namespace TextStatistics.Logic
{
    public static class DIConfig
    {
        public static void ConfigureLogicServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.ConfigureDalServices(configuration);
            services.AddTransient<IStatisticsService, StatisticsService>();
        }
    }
}