using System;
using System.Text;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PokemonPc.Infra;
using PokemonPc.Interfaces.Repositories;
using PokemonPc.Interfaces.Services;
using PokemonPc.Interfaces.Utils;
using PokemonPc.Models;
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

        // public static void ConfigureIndentityCore(this IServiceCollection services, string mongoConnectionString, string databaseName)
        // {
        //     services.AddIdentity<MongoIdentityUser, MongoIdentityRole>(options =>
        //     {
        //         options.SignIn.RequireConfirmedAccount = true;

        //         // Configurações de senha
        //         options.Password.RequireDigit = true;
        //         options.Password.RequireLowercase = true;
        //         options.Password.RequireUppercase = true;
        //         options.Password.RequireNonAlphanumeric = true;
        //         options.Password.RequiredLength = 8;
        //     })
        //     .AddMongoDbStores<MongoIdentityUser, MongoIdentityRole, Guid>(
        //         mongoConnectionString,
        //         databaseName
        //     )
        //     .AddDefaultTokenProviders();
        // }

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
                    ValidIssuer = configuration["Jwr:Issuer"],
                    ValidAudience = configuration["Jwr:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
                };
            });
        }

        public static void AddAplicationServices(this IServiceCollection services)
        {
            // Injeções Singleton
            services.AddSingleton<IAuthService, AuthService>();

            // Injeções Scopeds
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<ITrainerService, TrainerService>();
        }
    }
}

