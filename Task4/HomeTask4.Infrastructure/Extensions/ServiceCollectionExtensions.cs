using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<IRepository, EFRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CategoryController>();
            services.AddScoped<SubcategoryController>();
            services.AddScoped<IngredientController>();
            services.AddScoped<RecipeController>();
            return services;
        }
    }
}
