using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC_statistic.DataLayer.DTO.Statistic;

namespace SC_statistic.DataAccessLayer.DbConfigurations
{
    public class SessionDbConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> entity)
        {
            entity.ToTable("sessions");

            entity.HasKey(e => e.SessionId);

            entity.HasIndex(e => new { e.TrackedPlayerId, e.StartDate });

            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(12);
            entity.Property(e => e.TrackedPlayerId).IsRequired();

            entity.HasOne(s => s.TrackedPlayer)
                .WithMany(tp => tp.Sessions)
                .HasForeignKey(s => s.TrackedPlayerId)
                .HasPrincipalKey(tp => tp.TrackedPlayerId);
        }
    }
}