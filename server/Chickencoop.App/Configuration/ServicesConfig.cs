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

            services.AddSignalR();

            services.AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Chickencoop API" });
                });

            services.AddTransient<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            
            services.AddTransient<IPersonalLeaderboardService, PersonalLeaderboardService>();
            services.AddScoped<IPersonalLeaderboardRepository, PersonalLeaderboardRepository>();
        }
    }
}
