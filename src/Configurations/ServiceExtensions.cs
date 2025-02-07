using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PokemonPc.Infra;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Interfaces.Utils;
using PokemonPc.Repositories;
using PokemonPc.Services;

namespace PokemonPc.Configurations
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddScoped(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });
        }

        public static void ConfigureAuthenthication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
                };
            });
        }

        public static void AddAplicationServices(this IServiceCollection services)
        {
            // Injeções Singleton
            services.AddSingleton<IPokedexRepository, PokedexRepository>();
            services.AddSingleton<IPokedexService, PokedexService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IApiService, ApiService>();

            // Injeções Scopeds
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ITrainerService, TrainerService>();
        }
    }
}

