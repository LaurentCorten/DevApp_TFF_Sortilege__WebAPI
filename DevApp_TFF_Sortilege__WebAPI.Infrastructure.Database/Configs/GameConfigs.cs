using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Configs
{
    public class GameConfigs : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            // Table
            builder.ToTable("Parties");

            // PK
            builder.HasKey(g => g.Id)
                .HasName("PK_Parties");

            // Columns
            builder.Property(g => g.Id)
                .ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                .HasColumnName("Nom")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(g => g.CreatedDate)
                .HasColumnName("Date_Creation")
                .HasColumnType("DATETIME2")
                .IsRequired();

            // TODO : OneToMany !
            //builder.Property(g => g.PlayerA)
            //    .HasColumnName("JoueurA")
            //    .HasMaxLength(50)
            //    .IsUnicode()
            //    .IsRequired();

            //builder.Property(g => g.PlayerB)
            //    .HasColumnName("JoueurB")
            //    .HasMaxLength(50)
            //    .IsUnicode()
            //    .IsRequired(false);


        }
    }
}
