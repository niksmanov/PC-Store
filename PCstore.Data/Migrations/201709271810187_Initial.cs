namespace PCstore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CPU = c.String(nullable: false, maxLength: 30),
                        CpuSpeed = c.Double(nullable: false),
                        RAM = c.Int(nullable: false),
                        RamType = c.String(nullable: false, maxLength: 10),
                        HDD = c.Int(nullable: false),
                        GPU = c.String(nullable: false, maxLength: 30),
                        GpuMemory = c.Double(nullable: false),
                        OpticalDevice = c.String(nullable: false, maxLength: 30),
                        OperatingSystem = c.String(nullable: false, maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerPhone = c.String(nullable: false, maxLength: 12),
                        SellerEmail = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Seller_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Displays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Size = c.Double(nullable: false),
                        Resolution = c.String(nullable: false, maxLength: 9),
                        Type = c.String(nullable: false, maxLength: 5),
                        Colors = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerPhone = c.String(nullable: false, maxLength: 12),
                        SellerEmail = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Seller_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.Laptops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisplaySize = c.Double(nullable: false),
                        DisplayResolution = c.String(nullable: false, maxLength: 9),
                        CPU = c.String(nullable: false, maxLength: 30),
                        CpuSpeed = c.Double(nullable: false),
                        RAM = c.Int(nullable: false),
                        RamType = c.String(nullable: false, maxLength: 10),
                        HDD = c.Int(nullable: false),
                        GPU = c.String(nullable: false, maxLength: 30),
                        GpuMemory = c.Double(nullable: false),
                        Battery = c.Boolean(nullable: false),
                        OpticalDevice = c.String(nullable: false, maxLength: 30),
                        OperatingSystem = c.String(nullable: false, maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerPhone = c.String(nullable: false, maxLength: 12),
                        SellerEmail = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Seller_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Laptops", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Displays", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Computers", "Seller_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Laptops", new[] { "Seller_Id" });
            DropIndex("dbo.Laptops", new[] { "IsDeleted" });
            DropIndex("dbo.Displays", new[] { "Seller_Id" });
            DropIndex("dbo.Displays", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "IsDeleted" });
            DropIndex("dbo.Computers", new[] { "Seller_Id" });
            DropIndex("dbo.Computers", new[] { "IsDeleted" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Laptops");
            DropTable("dbo.Displays");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Computers");
        }
    }
}
