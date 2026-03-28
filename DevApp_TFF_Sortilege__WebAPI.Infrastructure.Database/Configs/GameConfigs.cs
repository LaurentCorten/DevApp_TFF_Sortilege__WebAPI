using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Configs
{
    public class GameConfigs : IEntityTypeConfiguration<Game> //TODO : Mettre à jour !!!
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

            builder.Property(g => g.CreationDate)
                .HasColumnName("Date_Creation")
                .HasColumnType("DATETIME2")
                .IsRequired();

            // Index & Unique
            builder.HasIndex(g => g.Name)
                .IsUnique()
                .HasDatabaseName("IDX_Parties__Nom");
            
            // Relations
            builder
                .HasOne(g => g.PlayerA)
                .WithMany()
                .HasForeignKey("Id_Joueur_A")
                .HasConstraintName("FK_Parties__Membres_A")
                .IsRequired();

            builder
                .HasOne(g => g.PlayerB)
                .WithMany()
                .HasForeignKey("Id_Joueur_B")
                .HasConstraintName("FK_Parties__Membres_B")
                .IsRequired(false);

            builder
                .HasOne(g => g.Winner)
                .WithMany()
                .HasForeignKey("Id_Joueur_Gagnant")
                .HasConstraintName("FK_Parties__Membres_Gagnant")
                .IsRequired(false);


        }
    }
}
