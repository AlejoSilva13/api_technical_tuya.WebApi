using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Domain.Interfaces;
using api_technical_tuya.Infrastructure.Persistence;
using api_technical_tuya.Infrastructure.Repositories;
using api_technical_tuya.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var cs = config.GetConnectionString("SqlServer")
                     ?? throw new InvalidOperationException("Missing connection string 'SqlServer'");

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
