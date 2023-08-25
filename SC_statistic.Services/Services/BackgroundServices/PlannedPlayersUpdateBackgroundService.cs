using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SC_statistic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.Services.Services.BackgroundServices
{
    public class PlannedPlayersUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public PlannedPlayersUpdateBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _services.CreateScope())
                    {
                        var plannedPlayersUpdateService = scope.ServiceProvider.GetRequiredService<IPlannedPlayersUpdateService>();
                        //await plannedPlayersUpdateService.UpdateAllPlayers();
                    }

                    await Task.Delay(TimeSpan.FromHours(8), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                //Заглушка
            }
        }
    }
}
