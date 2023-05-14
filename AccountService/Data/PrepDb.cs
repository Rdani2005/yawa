using AccountService.Models;
using AccountService.Repositories;
using AccountService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                SeedData(context, isProduction);

                var grpcClient = serviceScope.ServiceProvider.GetService<IUserDataClient>();
                var users = grpcClient.GetAllUsers();
                SeedUsers(serviceScope.ServiceProvider.GetService<IUserRepo>(), users);
                context.SaveChanges();

            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Applying migrations");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Couldn't run migrations: {ex}");
                }
            }

            if (context.Coins.Any())
            {
                Console.WriteLine("--> We have already some data!");
                return;
            }
            Console.WriteLine("--> Sedding Data!");
            context.Coins.AddRange(
                new CoinType()
                {
                    Name = "Dolar"
                },
                new CoinType()
                {
                    Name = "Colon"
                }
            );

            context.AccountTypes.AddRange(
                new AccountType() { Name = "Ahorros" },
                new AccountType() { Name = "Corriente" }
            );

            context.SaveChanges();
            return;
        }

        private static void SeedUsers(IUserRepo repo, IEnumerable<User> users)
        {
            Console.WriteLine("--> Adding the new users...");
            if (users == null)
            {
                Console.WriteLine("No habian usuarios.");
                return;
            }
            foreach (User user in users)
            {
                if (!repo.ExternalUserExists(user.ExternalID))
                    repo.AddUser(user);
            }
        }
    }
}