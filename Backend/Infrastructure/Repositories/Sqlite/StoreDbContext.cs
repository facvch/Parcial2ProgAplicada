using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sqlite
{
    /// <summary>
    /// Contexto de almacenamiento en base de datos. Aca se definen los nombres de 
    /// las tablas, y los mapeos entre los objetos
    /// </summary>
    public class StoreDbContext : DbContext
    {
        //public DbSet<DummyEntity> DummyEntity { get; set; }
        //public DbSet<Alumno> Alumno { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        public DbSet<Automovil> Automoviles { get; set; }


        protected StoreDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<DummyEntity>().ToTable("DummyEntity"); 
            // modelBuilder.Entity<Alumno>().ToTable("Alumno");
            modelBuilder.Entity<Automovil>().ToTable("Automoviles");
            modelBuilder.Entity<Automovil>()
                .HasIndex(a => a.NumeroMotor)
                .IsUnique();

            modelBuilder.Entity<Automovil>()
                .HasIndex(a => a.NumeroChasis)
                .IsUnique();
        }
    }
}
