using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TextStatistics.Dal.DataServices;
using TextStatistics.Definition.DataServices;

namespace TextStatistics.Dal
{
    public static class DIConfig
    {
        public static void ConfigureDalServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            //Add Database here, like
//#if DEBUG
//            services.AddDbContext<TextStatisticsContext>(opt =>  opt.UseInMemoryDatabase("TextStatistics"));
//#else
            services.AddDbContext<TextStatisticsContext>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
//#endif
            services.AddTransient<IStatisticDataService, StatisticDataService>();
        }
    }
}
