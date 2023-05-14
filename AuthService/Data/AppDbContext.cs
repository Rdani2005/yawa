using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Permition> Permitions { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<PermitionRol> PermitionRols { get; set; }
        public DbSet<User> Users { get; set; }


        /// Resume
        //      --> Defining all the Query for user permitions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Permition>()
                .HasMany(r => r.PermitionRols)
                .WithOne(pr => pr.Permition)
                .HasForeignKey(pr => pr.PermitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Users)
                .WithOne(u => u.UserRole)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.PermitionRols)
                .WithOne(pr => pr.Rol)
                .HasForeignKey(pr => pr.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermitionRol>()
                .HasKey(pr => new { pr.PermitionId, pr.RolId });

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}