using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SC_statistic.DataLayer.Entities;
using SC_statistic.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataAccessLayer.DbConfigurations
{
    public class NotificationDbConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> entity)
        {
            entity.ToTable("notifications");

            entity.HasKey(e => e.NotificationId);

            entity.HasIndex(e => e.Date, "Date_index");

            entity.Property(e => e.Text).IsRequired();
            entity.Property(e => e.Date).IsRequired();

        }
    }
}
