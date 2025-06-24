using Microsoft.EntityFrameworkCore;
using PersonaAPI.Models;

namespace PersonaAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Persona> personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir la primary key para la entidad Persona
            modelBuilder.Entity<Persona>(entity => { entity.HasKey(e => e.IdPersona); });
        }
    }
}
