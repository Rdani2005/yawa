using Microsoft.EntityFrameworkCore;
using MovementService.Models;
using MovementService.Repos;
using MovementService.SyncDataService.Grpc;

namespace MovementService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                SeedData(context, isProduction);


                var grpcClient = serviceScope.ServiceProvider.GetService<IAccountDataService>();
                var accounts = grpcClient.GetAllAccounts();
                SeedAccounts(serviceScope.ServiceProvider.GetService<IAccountRepo>(), accounts);
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

            if (context.Types.Any())
            {
                Console.WriteLine("--> We have already some data!");
                return;
            }
            Console.WriteLine("--> Sedding Data!");
            context.Types.AddRange(
                new MovementType() { Name = "Deposito" },
                new MovementType() { Name = "Retiro" },
                new MovementType() { Name = "Pago Servicios" }
            );
            context.SaveChanges();
            return;
        }


        private static void SeedAccounts(IAccountRepo repo, IEnumerable<Account> accounts)
        {
            Console.WriteLine("--> Adding new accounts");

            if (accounts == null)
            {
                Console.WriteLine("-- Not available accounts");
                return;
            }


            foreach (Account account in accounts)
            {
                if (!repo.ExternalAccountExists(account.ExternalId))
                    repo.CreateAccount(account);
            }
        }

    }
}