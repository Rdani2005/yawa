using AuthorizeService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizeService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }
        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("Applying Migrations!");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Couldn't run migrations: {ex}");
                }
            }

            AddPermitions(context);
            AddRoles(context);
            AddReferences(context);
        }

        private static void AddPermitions(AppDbContext context)
        {
            if (context.Permitions.Any())
            {
                Console.WriteLine("--> We have some Permitions. Not doing anything");
                return;
            }
            Console.WriteLine("--> Adding new Permitions!");
            context.Permitions.AddRange(
                new Permition() { Name = "Find Own Accounts", Description = "Find selfuser Accounts", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Create Personal Movements", Description = "Make movements to personal accounts", PermitionRols = new List<PermitionRol>() },

                new Permition() { Name = "Find external Accounts", Description = "Find external Accounts", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Create Clients", Description = "Add New Clients", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Delete Clients", Description = "Delete Clients", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Find Clients", Description = "Find Clients", PermitionRols = new List<PermitionRol>() },

                new Permition() { Name = "Create Accounts", Description = "Create new Accounts", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Delete Accounts", Description = "Delete Accounts", PermitionRols = new List<PermitionRol>() },

                new Permition() { Name = "Create External Movements", Description = "Make External movements", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Delete Movements", Description = "Delete movements", PermitionRols = new List<PermitionRol>() },


                new Permition() { Name = "Create Users", Description = "Add new users", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Delete Users", Description = "Add new users", PermitionRols = new List<PermitionRol>() },
                new Permition() { Name = "Find Users", Description = "Add new users", PermitionRols = new List<PermitionRol>() }
            );

            context.SaveChanges();
        }

        private static void AddRoles(AppDbContext context)
        {
            if (context.Rols.Any())
            {
                Console.WriteLine("--> We have some Rols. Not doing anything");
                return;
            }
            Console.WriteLine("--> Adding new Roles");
            context.Rols.AddRange(
                new Rol() { Name = "Administrador", Description = "Administrador user role", PermitionRols = new List<PermitionRol>() },
                new Rol() { Name = "Collaborator", Description = "Collaborator user role", PermitionRols = new List<PermitionRol>() },
                new Rol() { Name = "client", Description = "App Client user role", PermitionRols = new List<PermitionRol>() }
            );
            context.SaveChanges();
        }

        private static void AddReferences(AppDbContext context)
        {
            if (context.PermitionRols.Any())
            {
                Console.WriteLine("--> All relations are done. Not doing anything");
                return;
            }
            Console.WriteLine("--> Adding all relations on BD");
            AddAdminPermitions(context);
            AddCollaboratorPermitions(context);
            AddClientPermitions(context);
            context.SaveChanges();
        }

        private static void AddAdminPermitions(AppDbContext context)
        {
            foreach (Permition permition in context.Permitions.ToList())
            {
                permition.PermitionRols.Add(
                    new PermitionRol() { Rol = context.Rols.Find(1) }
                );
            }

            context.SaveChanges();
        }

        private static void AddCollaboratorPermitions(AppDbContext context)
        {
            var collaborator = context.Rols.Find(2);

            for (int i = 1; i <= 10; i++)
            {
                collaborator.PermitionRols.Add(
                    new PermitionRol() { Permition = context.Permitions.Find(i) }
                );
                // Console.WriteLine($"Permition --> {context.Permitions.Find(i).Name}");
            }

            context.SaveChanges();
        }

        private static void AddClientPermitions(AppDbContext context)
        {
            var client = context.Rols.Find(3);

            for (int i = 1; i <= 2; i++)
            {
                client.PermitionRols.Add(
                    new PermitionRol() { Permition = context.Permitions.Find(i) }
                );
            }

            context.SaveChanges();
        }
    }
}