using Microsoft.EntityFrameworkCore;
using ReservationApp1.Models;

namespace ReservationApp1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer(@"Data Source=DESKTOP-O9T5PM1\SQL19;Initial Catalog=ReservationRestaurantDB;Integrated Security=True;Encrypt = false;");


        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Zone> Zones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Zone)
                .WithMany()
                .HasForeignKey(r => r.ZoneId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
