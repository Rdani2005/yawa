using Microsoft.EntityFrameworkCore;
using MovementService.Models;

namespace MovementService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<MovementType> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountsRelations(modelBuilder);
            TypesRelation(modelBuilder);
            MovementsRelations(modelBuilder);
        }

        private void AccountsRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Movements)
                .WithOne(m => m.Account)
                .HasForeignKey(m => m.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void TypesRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovementType>()
                .HasMany(t => t.Movements)
                .WithOne(m => m.Type)
                .HasForeignKey(m => m.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        private void MovementsRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Account)
                .WithMany(a => a.Movements)
                .HasForeignKey(m => m.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Type)
                .WithMany(t => t.Movements)
                .HasForeignKey(m => m.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}