using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SC_statistic.DataAccessLayer;
using SC_statistic.DataAccessLayer.Interfaces;
using SC_statistic.DataAccessLayer.Repositories;
using SC_statistic.DataLayer.Mappers;
using SC_statistic.DataLayer.Mappers.Corporations;
using SC_statistic.DataLayer.Mappers.PlayerHistory;
using SC_statistic.Services.Interfaces;
using SC_statistic.Services.Services;

namespace SC_statistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson();
            builder.Services.AddAntiforgery(options => { options.SuppressXFrameOptionsHeader = true; });

            string connection = builder.Configuration.GetConnectionString("MySqlConnection");
            string version = builder.Configuration.GetConnectionString("MySqlVersion");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connection, ServerVersion.Parse(version)));

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

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<INotificationService, NotificationService>();
            builder.Services.AddTransient<IPlayerService, PlayerService>();
            builder.Services.AddTransient<ICorporationService, CorporationService>();
            builder.Services.AddSingleton<ISC_ApiService, SC_ApiService>();


            builder.Services.AddAutoMapper(
                typeof(NotificationToNotificationDTOMapperProfile),
                typeof(CorpStatToCorporationMapperProfile),
                typeof(FullStatToPlayerMapperProfile),
                typeof(PlayerNicknameHistoryToPlayerNicknameHistoryDTOMapperProfile),
                typeof(PlayerCorporationHistoryToPlayerCorporationHistoryDTOMapperProfile),
                typeof(PlayerToPlayerHistoryDTOMapperProfile),
                typeof(CorporationToCorporationDTOMapperProfile)
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
    }
}