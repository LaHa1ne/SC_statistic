using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SC_statistic.DataAccessLayer;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.Mappers.Corporations;
using SC_statistic.DataLayer.Mappers.Notifications;
using SC_statistic.DataLayer.Mappers.PlayerHistory;
using SC_statistic.DataLayer.Mappers.PlayerStatistic;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;
using SC_statistic.Services.Services.BackgroundServices;
using System.Linq.Expressions;

namespace SC_statistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration(args);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson();
            builder.Services.AddAntiforgery(options => { options.SuppressXFrameOptionsHeader = true; });


            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            string connection = builder.Configuration.GetConnectionString("PostgresSQL");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connection));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
            builder.Services.AddTransient<ICorporationRepository, CorporationRepository>();
            builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
            builder.Services.AddTransient<ITrackedPlayerRepository, TrackedPlayerRepository>();
            builder.Services.AddTransient<ISessionRepository, SessionRepository>();

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<INotificationService, NotificationService>();
            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddTransient<IPlannedPlayersUpdateService, PlannedPlayersUpdateService>();
            builder.Services.AddTransient<ICorporationService, CorporationService>();
            builder.Services.AddSingleton<ISC_ApiService, SC_ApiService>();

            builder.Services.AddHostedService<PlannedPlayersUpdateBackgroundService>();


            builder.Services.AddAutoMapper(
                typeof(NotificationToNotificationDTOMapperProfile),
                typeof(CorpStatToCorporationMapperProfile),
                typeof(FullStatToPlayerMapperProfile),
                typeof(PlayerNicknameHistoryToPlayerNicknameHistoryDTOMapperProfile),
                typeof(PlayerCorporationHistoryToPlayerCorporationHistoryDTOMapperProfile),
                typeof(PlayerToPlayerHistoryDTOMapperProfile),
                typeof(CorporationToCorporationDTOMapperProfile),
                typeof(FullStatToFullStatDTOMapperProfile),
                typeof(UserToTrackedPlayerListDTOMapperProfile),
                typeof(SessionToSessionShortInfoDTOMapperProfile),
                typeof(FullStatToCheckpointStatMapperProfile),
                typeof(SessionToSessionDTOMapperProfile),
                typeof(SessionToAddCheckpointDTOMapperProfile)
                );

            builder.Services.AddHttpClient();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static IConfiguration GetConfiguration(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            /*return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.docker.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();*/
        }
    }
}