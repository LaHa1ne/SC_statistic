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
    public class CorporationDbConfiguration : IEntityTypeConfiguration<Corporation>
    {
        public void Configure(EntityTypeBuilder<Corporation> entity)
        {

            entity.ToTable("corporations");

            entity.HasKey(e => e.CorporationId);

            entity.HasIndex(e => e.CurrentName);
            entity.HasIndex(e => e.CurrentTag);

            entity.Property(e => e.CorporationId).IsRequired();
            entity.Property(e => e.CurrentName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CurrentTag).IsRequired().HasMaxLength(5);

        }
    }
}
