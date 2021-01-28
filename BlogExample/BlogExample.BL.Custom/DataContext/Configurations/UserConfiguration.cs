using BlogExample.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom.DataContext.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this
                .ToTable("Users")
                .HasKey(x => x.Id)
                .HasMany(p => p.Blogs)
                .WithRequired(p => p.User);
            this
                .HasIndex(x => x.Nickname)
                .IsUnique();
            this
                .Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this
                .Property(x => x.Nickname)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
