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
                var roleName = "Admin";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (!context.Computers.Any())
            {
                for (int i = 2; i < 10; i++)
                {
                    var computer = new Computer()
                    {
                        CPU = $"Intel Core I{i} {i}0{i}0",
                        CpuSpeed = double.Parse($"{i / 2}.{i}"),
                        RAM = i * 2,
                        RamType = $"DDR{i}",
                        HDD = i * 100,
                        GPU = $"Nvidia GeForce {i}{i}00",
                        GpuMemory = int.Parse($"{i / 2}"),
                        OpticalDevice = $"LG DVD-RW class {i}",
                        OperatingSystem = $"Windows {i}",
                        Price = decimal.Parse($"{i}{i}0"),
                        SellerPhone = $"088{i}9{i}6{i}4{i}",
                        Description = "Good computer for this price!",
                        Seller = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Computers.Add(computer);
                }
            }

            if (!context.Laptops.Any())
            {
                for (int i = 2; i < 10; i++)
                {
                    var laptop = new Laptop()
                    {
                        DisplaySize = double.Parse($"{i}.{i}"),
                        DisplayResolution = $"{i * 2}00x{i}00",
                        CPU = $"Intel Core I{i} {i}0{i}0",
                        CpuSpeed = double.Parse($"{i / 2}.{i}"),
                        RAM = i * 2,
                        RamType = $"DDR{i}",
                        HDD = i * 100,
                        GPU = $"Nvidia GeForce {i}{i}00",
                        GpuMemory = int.Parse($"{i / 2}"),
                        Battery = "Li-Pol",
                        OpticalDevice = $"Samsung DVD-RW class {i}",
                        OperatingSystem = $"Windows {i}",
                        Price = decimal.Parse($"{i}{i}0"),
                        SellerPhone = $"087{i}5{i}2{i}9{i}",
                        Description = "Good laptop for this price!",
                        Seller = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };
                    context.Laptops.Add(laptop);
                }
            }

            if (!context.Displays.Any())
            {
                for (int i = 2; i < 10; i++)
                {
                    var display = new Display()
                    {
                        Size = double.Parse($"{i}.{i}"),
                        Resolution = $"{i * 2}00x{i}00",
                        Type = "IPS",
                        Colors = double.Parse($"{i}{i}{i}0000"),
                        Price = decimal.Parse($"{i}{i}0"),
                        SellerPhone = $"089{i}2{i}6{i}4{i}",
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
