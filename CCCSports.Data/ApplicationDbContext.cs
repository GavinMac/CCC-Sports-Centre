using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using static CCCSports.Data.Migrations.DbMigrationsConfig;
using CCCSports.Data.FacilityClasses;

namespace CCCSports.Data
{
    /// <summary>
    /// AppplicationDbContext - Creates database and configures it
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<FacilitiesClasses.CafeItem> CafeStock { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<HallActivityBooking> HallBookings { get; set; }

        public DbSet<RoomBooking> RoomBookings { get; set; }

        public DbSet<EquipmentBooking> EquipmentBookings { get; set; }

        public DbSet<EquipmentHireCart> HireCarts { get; set; }

        public DbSet<CentreMembership> Memberships { get; set; }

        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }





        public ApplicationDbContext()
            : base("CCCSportsConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}