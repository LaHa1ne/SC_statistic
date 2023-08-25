using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC_statistic.DataLayer.DTO.Statistic;
using SC_statistic.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.DbConfigurations
{
    public class CheckpointDbConfiguration : IEntityTypeConfiguration<Checkpoint>
    {
        public void Configure(EntityTypeBuilder<Checkpoint> entity)
        {
            entity.ToTable("checkpoints");

            entity.HasKey(e => e.CheckpointId);

            entity.HasIndex(e => new { e.SessionId, e.Date });

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.SessionId).IsRequired();

            entity.OwnsOne(e => e.CheckpointStat);

            entity.HasOne(c => c.Session)
                .WithMany(s => s.Checkpoints)
                .HasForeignKey(c => c.SessionId)
                .HasPrincipalKey(s => s.SessionId);
        }
    }
}