using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfiguredDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration["Configurations:UseDatabase"];
            services.CreateDataBase(dbType, configuration);
        }
    }
}
