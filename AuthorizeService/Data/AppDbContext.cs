using AuthorizeService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizeService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Permition> Permitions { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<PermitionRol> PermitionRols { get; set; }


        /// Resume
        //      --> Defining all the Query for user permitions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Permition>()
                .HasMany(r => r.PermitionRols)
                .WithOne(pr => pr.Permition)
                .HasForeignKey(pr => pr.PermitionId);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.PermitionRols)
                .WithOne(pr => pr.Rol)
                .HasForeignKey(pr => pr.RolId);

            modelBuilder.Entity<PermitionRol>()
                .HasKey(pr => new { pr.PermitionId, pr.RolId });
        }
    }
}