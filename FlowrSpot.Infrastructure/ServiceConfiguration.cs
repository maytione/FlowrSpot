using FlowrSpot.Application.Common.Interfaces;
using FlowrSpot.Application.Flowers.Interfaces;
using FlowrSpot.Application.Likes.Interfaces;
using FlowrSpot.Application.Sightings.Interfaces;
using FlowrSpot.Application.Users.Interfaces;

using FlowrSpot.Infrastructure.Data;
using FlowrSpot.Infrastructure.Data.Identity;
using FlowrSpot.Infrastructure.Data.Identity.Services;
using FlowrSpot.Infrastructure.Data.Repository;
using FlowrSpot.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceConfiguration
{
    /// <summary>
    /// AddInfrastructureServices - Setup services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // register IApplicationDbContext interface
        services.AddScoped<IApplicationDbContext>(provider => provider
            .GetRequiredService<ApplicationDbContext>());

        // Register Migration database class
        services.AddScoped<ApplicationDbContextInit>();

        // register IIdentityRepository interface (for User register and login)
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        // register Flower interface
        services.AddScoped<IFlowerRepository, FlowerRepository>();
        // register Sighting interface
        services.AddScoped<ISightingRepository, SightingRepository>();
        // register Like interface
        services.AddScoped<ILikeRepository, LikeRepository>();

        // register repository pattern engine
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(BaseRepository<>));

        // Configure JWT settings - Nothing smart, just fetch from configuration
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        // Register JWT required interfaces
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();



        // Setup database connection (in memory or postgresql)
        bool useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

        if (useInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) => options
                .UseInMemoryDatabase("FlowrSpotInMemoryDb")
                .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()));
        }
        else 
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) => options
                .UseNpgsql(configuration.GetConnectionString("Default")!)
                .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()));
        }



        // Setup Core Identity User with default IdentityRole
        // Entity framework stores (user store, role store)
        // Setup default token providers (EmailTokenProvider, PhoneNumberTokenProvider,...)
        // Add default api endpoints for identity user
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddApiEndpoints();

        return services;
    }
}
