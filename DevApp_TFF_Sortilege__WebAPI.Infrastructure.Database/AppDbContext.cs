using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database
{
    // Configuration de la DB avec EF Core
    public class AppDbContext : DbContext
    {
        // Ensemble des tables
        public DbSet<Member> Members { get; set; }


        // Définition du Ctor nécessaire à l'injection de dépendance
        public AppDbContext(DbContextOptions options) : base(options) { } // TODO : Question : Pq dans la doc ils mettent AppDbContext(DbContextOptions<AppDbContext> options) et nous on ne met pas le <T> ??

        // Instructions pour implémenter les configs "IEntityTypeConfiguration<>" en mode 'auto-detection' dans tout l'assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

