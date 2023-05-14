using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CoinType> Coins { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountRelation(modelBuilder);
            CoinRelation(modelBuilder);
            TypeRelation(modelBuilder);
            UserRelation(modelBuilder);
        }


        private void AccountRelation(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Account>()
                    .HasOne(a => a.Type)
                    .WithMany(T => T.Accounts)
                    .HasForeignKey(a => a.TypeId)
                    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<Account>()
                    .HasOne(a => a.Coin)
                    .WithMany(c => c.Accounts)
                    .HasForeignKey(a => a.CoinId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Account>()
                    .HasOne(a => a.User)
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }

        private void CoinRelation(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CoinType>()
                    .HasMany(c => c.Accounts)
                    .WithOne(a => a.Coin)
                    .HasForeignKey(a => a.CoinId)
                    .OnDelete(DeleteBehavior.Cascade);
        }

        private void TypeRelation(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AccountType>()
                    .HasMany(t => t.Accounts)
                    .WithOne(a => a.Type)
                    .HasForeignKey(a => a.TypeId)
                    .OnDelete(DeleteBehavior.Cascade);
        }

        private void UserRelation(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}