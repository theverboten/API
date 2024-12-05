using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Components.Routing;


namespace API.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            return services;
        }
    }
}