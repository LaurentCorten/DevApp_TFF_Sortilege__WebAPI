using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database
{
    // Db config with EF Core
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<Member> Members { get; set; }


        // Definition DI's ctor
        public AppDbContext(DbContextOptions options) : base(options) { } // TODO : Question : Pq dans la doc ils mettent AppDbContext(DbContextOptions<AppDbContext> options) et nous on ne met pas le <T> ??

        // Instructions for implementing "IEntityTypeConfiguration<>" config with 'auto-detection' in the whole assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

