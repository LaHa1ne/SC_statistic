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
    internal class PlayerCorporationHistoryDbConfiguration : IEntityTypeConfiguration<PlayerCorporationHistory>
    {
        public void Configure(EntityTypeBuilder<PlayerCorporationHistory> entity)
        {

            entity.ToTable("playercorporationhistories");

            entity.HasKey(e => e.PlayerCorporationHistoryId);

            entity.HasIndex(e => new { e.PlayerId, e.Date }).IsUnique();

            entity.Property(e => e.PlayerId).IsRequired();
            entity.Property(e => e.Date).IsRequired();

            entity.HasOne(ph => ph.Player)
                .WithMany(p => p.CorporationHistory)
                .HasForeignKey(ph => ph.PlayerId)
                .HasPrincipalKey(p => p.PlayerId);

            entity.HasOne(ph => ph.Corporation)
                .WithMany(c => c.CorporationHistories)
                .HasForeignKey(ph => ph.CorporationId)
                .HasPrincipalKey(c => c.CorporationId);

        }
    }
}
