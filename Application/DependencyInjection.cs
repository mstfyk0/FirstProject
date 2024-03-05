
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IMealsService, MealsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            return services;

        }
    }
}
