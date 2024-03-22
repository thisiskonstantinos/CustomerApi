
using Data.EF;
using Data.EF.Infrastructure;
using Data.EF.Services;
using Domain.Abstractions;
using Domain.Logic;
using Microsoft.EntityFrameworkCore;

namespace Api.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppDb");

            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlite(connectionString);
                options.EnableSensitiveDataLogging();
            })
                .AddScoped<ICustomerContext, CustomerContext>();

            services.AddAutoMapper(expression =>
            {
                expression.AllowNullCollections = true;
            }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerDataService, CustomerDataService>();

            return services;
        }
    }
}
