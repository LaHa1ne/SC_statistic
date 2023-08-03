using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.DbConfigurations
{
    public class PlayerDbConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> entity)
        {

            entity.ToTable("players");

            entity.HasKey(e => e.PlayerId);

            entity.HasIndex(e => new { e.CurrentNickname, e.IsInformationCorrect });
            entity.HasIndex(e => e.CurrentCorporationId);

            entity.Property(e => e.CurrentNickname).IsRequired().HasMaxLength(16);

            entity.HasOne(p => p.CurrentCorporation)
                .WithMany(C => C.Players)
                .HasForeignKey(P => P.CurrentCorporationId)
                .HasPrincipalKey(c => c.CorporationId);

        }

    }
}
