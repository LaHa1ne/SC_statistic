using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SC_statistic.DataAccessLayer.DbConfigurations;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SC_statistic"))
                    .AddJsonFile("appsettings.json", optional: true).Build();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseMySql(config.GetConnectionString("MySqlConnection"), ServerVersion.Parse(config.GetConnectionString("MySqlVersion")), b => b.MigrationsAssembly("SC_statistic"));

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Corporation> Corporations { get; set; } = null!;
        public virtual DbSet<PlayerNicknameHistory> NicknameHistories { get; set; } = null!;
        public virtual DbSet<PlayerCorporationHistory> CorporationHistories { get; set; } = null!;

        public virtual DbSet<TrackedPlayer> TrackedPlayers { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Checkpoint> Checkpoints { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationDbConfiguration());
            modelBuilder.ApplyConfiguration(new CorporationDbConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerDbConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerNicknameHistoryDbConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerCorporationHistoryDbConfiguration());
            modelBuilder.ApplyConfiguration(new TrackedPlayerDbConfiguration());
            modelBuilder.ApplyConfiguration(new SessionDbConfiguration());
            modelBuilder.ApplyConfiguration(new CheckpointDbConfiguration());

        }
    }
}
