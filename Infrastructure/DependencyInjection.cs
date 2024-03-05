
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validators;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, SQLProductsRepository>();
            services.AddScoped<IMealsRepository, SQLMealsRepository>();
            services.AddScoped<IUsersRepository, SQLUsersRepository>();

            services.AddScoped<IValidator<Meal>, MealValidator>();
            services.AddScoped<IValidator<Product>, ProductValidator>();
            services.AddScoped<IValidator<User>, UserValidator>();

            return services;
        }

    }
}
