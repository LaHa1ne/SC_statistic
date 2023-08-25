using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.DbConfigurations
{
    public class TrackedPlayerDbConfiguration : IEntityTypeConfiguration<TrackedPlayer>
    {
        public void Configure(EntityTypeBuilder<TrackedPlayer> entity)
        {
            entity.ToTable("trackedplayers");

            entity.HasKey(e => e.TrackedPlayerId);

            entity.HasIndex(e => new { e.UserId, e.PlayerId }).IsUnique();

            entity.Property(e => e.PlayerId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne(tp => tp.User)
                .WithMany(u => u.TrackedPlayers)
                .HasForeignKey(tp => tp.UserId)
                .HasPrincipalKey(u => u.UserId);

            entity.HasOne(tp => tp.Player)
                .WithMany(u => u.TrackedPlayers)
                .HasForeignKey(tp => tp.PlayerId)
                .HasPrincipalKey(u => u.PlayerId);
        }
    }
}
