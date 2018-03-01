namespace CCCSports.Data.Migrations
{
    using FacilitiesClasses;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<CCCSports.Data.ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CCCSports.Data.ApplicationDbContext";
        }

        /// <summary>
        /// Seeds the database
        /// </summary>
        public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                //  This method will be called after migrating to the latest version.
                // Seed initial data only if the database is empty

                if (!context.Users.Any())
                {

                    //Declares identity variables
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var userStore = new UserStore<ApplicationUser>(context);


                    // Populating the Role table

                    //Admin
                    if (!roleManager.RoleExists(Roles.ROLE_ADMIN))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_ADMIN));
                    }

                    //Manager
                    if (!roleManager.RoleExists(Roles.ROLE_MANAGER))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_MANAGER));
                    }

                    //Bookings Clerk
                    if (!roleManager.RoleExists(Roles.ROLE_CLERK))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_CLERK));
                    }

                    //Membership Clerk
                    if (!roleManager.RoleExists(Roles.ROLE_MCLERK))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_MCLERK));
                    }

                    //Staff members
                    if (!roleManager.RoleExists(Roles.ROLE_STAFF))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_STAFF));
                    }

                    //Customer members
                    if (!roleManager.RoleExists(Roles.ROLE_MEMBER))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_MEMBER));
                    }

                    //Basic users
                    if (!roleManager.RoleExists(Roles.ROLE_USER))
                    {
                        var roleresult = roleManager.Create(new IdentityRole(Roles.ROLE_USER));
                    }

                    context.SaveChanges();



                    ///////////////////////////////////////////////////////////////////////////////////////

                    //Creating default Admin and User accounts

                    //Admin
                    var adminUser = userManager.FindByName("admin@admin.com");

                    if (adminUser == null)
                    {

                        var newAdmin = new ApplicationUser()
                        {
                            FirstName = "Gavin",
                            LastName = "Macleod",
                            UserName = "admin@admin.com",
                            InputUserName = "Admin",
                            Gender = "Male",
                            DateOfBirth = new DateTime(1997, 04, 23),
                            Email = "admin@admin.com",
                            HouseNumber = "01",
                            Street = "Admin Street",
                            Town = "Adminton",
                            PostCode = "A1 1MN",
                            Membership = new CentreMembership(),
                            EmailConfirmed = true

                        };

                        newAdmin.Membership.MemberId = newAdmin.Id;
                        newAdmin.Membership.MemberName = newAdmin.FirstName;
                        newAdmin.Membership.MembershipUser = newAdmin;
                        newAdmin.Membership.MemberType = "Adult";

                        userManager.Create(newAdmin, "admin123");
                        userManager.AddToRole(newAdmin.Id, Roles.ROLE_ADMIN);
                    }

                    ///////////////////////////////////////////////////////////////////////////////////////


                    // Normal user role and account
                    var normalUser = userManager.FindByName("user@user.com");

                    if (normalUser == null)
                    {

                        //Creates a user
                        var newUser = new ApplicationUser()
                        {
                            FirstName = "Example",
                            LastName = "User",
                            PhoneNumber = "012345678",
                            UserName = "user@user.com",
                            InputUserName = "User",
                            Gender = "Male",
                            DateOfBirth = DateTime.Now,
                            Email = "user@user.com",
                            HouseNumber = "02",
                            Street = "User Street",
                            Town = "Userton",
                            PostCode = "U1 1SR",
                            Membership = new CentreMembership(),
                            EmailConfirmed = true

                        };

                        newUser.Membership.MemberId = newUser.Id;
                        newUser.Membership.MemberName = newUser.FirstName;
                        newUser.Membership.MembershipUser = newUser;
                        newUser.Membership.MemberType = "Adult";

                        userManager.Create(newUser, "user123");
                        userManager.AddToRole(newUser.Id, Roles.ROLE_USER);
                    }

                    context.SaveChanges();
                }

                CreateExampleProducts(context);
                base.Seed(context);
                context.SaveChanges();

            }

        }

        /// <summary>
        /// Creates some products and adds them to the database
        /// </summary>
        /// <param name="context"></param>
        private static void CreateExampleProducts(ApplicationDbContext context)
        {
            context.Products.Add(new Product()
            {
                ProductName = "Football",
                ProductDesc = "A high quality leather outdoor football",
                ProductCost = 4.99,
                ProductRating = 4,
                ProductStock = 20

            });


            context.CafeStock.Add(new CafeItem()
            {
                ItemName = "Coke Can",
                ItemCost = 1.50,
                ItemStock = 50
            });

            context.SaveChanges();

        }

    }

}