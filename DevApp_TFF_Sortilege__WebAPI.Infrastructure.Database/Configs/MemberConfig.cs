using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Configs
{
    // Detailed instructions to set "IEntityTypeConfiguration<>" properly
    internal class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Table
            builder.ToTable("Membres");

            // PK
            builder.HasKey(m => m.Id)
                .HasName("PK_Membres");

            // Columns
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .IsRequired(); // Most probably totally useless to specify but why not tho ?

            builder.Property(m => m.Name)
                .HasColumnName("Pseudo")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.Email)
                .HasMaxLength(320)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.HashWord)
                .HasColumnName("Hash_Mot")
                .HasMaxLength(200)
                .IsUnicode()
                .IsRequired();

            // Index - Uniques
            builder.HasIndex(m => m.Email)
                .IsUnique()
                .HasDatabaseName("UK_Membres__Email");

            builder.HasIndex(m => m.Name)
                .IsUnique()
                .HasDatabaseName("UK_Membres__Pseudo");
        }
    }
}
