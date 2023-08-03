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
    public class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {

            entity.ToTable("users");

            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.Login, "Login_UNIQUE")
               .IsUnique();

            entity.Property(e => e.Login).HasMaxLength(16);
            entity.Property(e => e.Password).HasMaxLength(120);

            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        }
    }
}
