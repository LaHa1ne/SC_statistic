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
    public class PlayerNicknameHistoryDbConfiguration : IEntityTypeConfiguration<PlayerNicknameHistory>
    {
        public void Configure(EntityTypeBuilder<PlayerNicknameHistory> entity)
        {

            entity.ToTable("playernicknamehistories");

            entity.HasKey(e => e.PlayerNicknameHistoryId);

            entity.HasIndex(e => new {e.PlayerId, e.Date}).IsUnique();

            entity.Property(e => e.PlayerId).IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Nickname).HasMaxLength(16);

            entity.HasOne(ph => ph.Player)
                .WithMany(p => p.NicknameHistory)
                .HasForeignKey(ph => ph.PlayerId)
                .HasPrincipalKey(p => p.PlayerId);

        }
    }
}
