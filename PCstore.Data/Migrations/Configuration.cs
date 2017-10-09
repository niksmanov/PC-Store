namespace PCstore.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "admin@abv.bg";
        private const string AdministratorPassword = "admin123";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);

            base.Seed(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var adminRole = "Admin";
                var userRole = "User";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var firstRole = new IdentityRole { Name = adminRole };
                var secondRole = new IdentityRole { Name = userRole };

                roleManager.Create(firstRole);
                roleManager.Create(secondRole);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, adminRole);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            var random = new Random();
            if (!context.Computers.Any())
            {
                for (int i = 2; i < 50; i++)
                {
                    var computer = new Computer()
                    {
                        CPU = $"Intel Core I{i} {i}0{i}0",
                        CpuSpeed = random.Next(1, 5),
                        RAM = i,
                        RamType = $"DDR{i}",
                        HDD = i * 10,
                        GPU = $"Nvidia GeForce {i}{i}00",
                        GpuMemory = random.Next(1, 8),
                        OpticalDevice = $"LG DVD-RW class {i}",
                        OperatingSystem = $"Windows {i}",
                        Price = decimal.Parse($"{i}{i}"),
                        SellerPhone = $"088{i}9{i}64",
                        Description = "Good computer for this price!",
                        Seller = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Computers.Add(computer);
                }
            }

            if (!context.Laptops.Any())
            {
                for (int i = 2; i < 50; i++)
                {
                    var laptop = new Laptop()
                    {
                        DisplaySize = random.Next(1, 19),
                        DisplayResolution = $"{i * 2}00x{i}00",
                        CPU = $"Intel Core I{i} {i}0{i}0",
                        CpuSpeed = random.Next(1, 5),
                        RAM = i,
                        RamType = $"DDR{i}",
                        HDD = i * 10,
                        GPU = $"Nvidia GeForce {i}{i}00",
                        GpuMemory = random.Next(1, 8),
                        Battery = "Li-Pol",
                        OpticalDevice = $"Samsung DVD-RW class {i}",
                        OperatingSystem = $"Windows {i}",
                        Price = decimal.Parse($"{i}{i}"),
                        SellerPhone = $"087{i}5{i}23",
                        Description = "Good laptop for this price!",
                        Seller = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Laptops.Add(laptop);
                }
            }

            if (!context.Displays.Any())
            {
                for (int i = 2; i < 50; i++)
                {
                    var display = new Display()
                    {
                        Size = random.Next(1, 19),
                        Resolution = $"{i * 2}00x{i}00",
                        Type = "IPS",
                        Colors = random.Next(256000, 17000000),
                        Price = decimal.Parse($"{i}{i}"),
                        SellerPhone = $"089{i}2{i}64",
                        Description = "Good display for this price!",
                        Seller = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Displays.Add(display);
                }
            }
        }
    }
}
