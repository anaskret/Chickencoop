using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Chickencoop.DataAccess;
using Chickencoop.Services.Services.Interfaces;
using Chickencoop.Services.Services;
using Chickencoop.Repositories.Repositories.Interfaces;
using Chickencoop.Repositories.Repositories;
using Chickencoop.Models.Dtos.BaseDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using AutoMapper;
using Chickencoop.Services.Mapping;
using Chickencoop.Models.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Chickencoop.App.Configuration
{
    public class ServicesConfig
    {
        public static void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ChickencoopContext>(options =>
                  options.UseSqlServer(
                      configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            services.AddCors();

            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.ResponseType = configuration["Authentication:Cognito:ResponseType"];
                options.MetadataAddress = configuration["Authentication:Cognito:MetadataAddress"];
                options.ClientId = configuration["Authentication:Cognito:ClientId"];
            });

            services.AddSignalR();

            services.AddAutoMapper(typeof(PlayerProfile), typeof(PersonalLeaderboardProfile), typeof(LobbyProfile));

            services.AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Chickencoop API" });
                });

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            
            services.AddTransient<IPersonalLeaderboardService, PersonalLeaderboardService>();
            services.AddScoped<IPersonalLeaderboardRepository, PersonalLeaderboardRepository>();
            
            services.AddTransient<ILobbyService, LobbyService>();
            services.AddScoped<ILobbyRepository, LobbyRepository>();
        }
    }
}
