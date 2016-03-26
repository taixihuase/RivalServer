using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DatabaseServer.Entity.Models.Maps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(t => t.Id)
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("UserId");
            Property(t => t.Account).HasMaxLength(30).IsRequired().HasColumnName("Account");
            Property(t => t.Nickname).HasMaxLength(10).IsRequired().HasColumnName("Nickname");
            Property(t => t.Password).HasMaxLength(30).IsRequired().HasColumnName("Password");
            Property(t => t.RegistTime).IsRequired().HasColumnName("RegistTime");
        }
    }
}
