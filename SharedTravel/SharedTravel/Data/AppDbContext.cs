using Microsoft.EntityFrameworkCore;
using SharedTravel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(s => s.Username).IsUnique();

            builder.Entity<UserTravel>().HasKey(s => new { s.UserId, s.TravelId });
            builder.Entity<UserTravel>().HasOne(s => s.Travel).WithMany(s => s.UserTravels).HasForeignKey(s => s.TravelId);
            builder.Entity<UserTravel>().HasOne(s => s.User).WithMany(s => s.UserTravels).HasForeignKey(s => s.UserId);

            builder.Entity<PendingInvite>().HasKey(s => new { s.UserId, s.TravelId });
            builder.Entity<PendingInvite>().HasOne(s => s.Travel).WithMany(s => s.PendingInvites).HasForeignKey(s => s.TravelId);
            builder.Entity<PendingInvite>().HasOne(s => s.User).WithMany(s => s.PendingInvites).HasForeignKey(s => s.UserId);

            builder.Entity<User>().HasData(
                new User { UserId = 1, Username = "gladitorian",  Password = "gladitorianpass", FirstName = "Martin", LastName = "Vasilev"},
                new User { UserId = 2, Username = "nikiv", Password = "nikipass", FirstName = "Nikola", LastName = "Valchanov" }
            );

            builder.Entity<Travel>().HasData(
                new Travel { TravelId = 1, CreatorId = 2, CityFrom = "Plovdiv", AddressFrom = "bul Bulgaria 236c", DepartureTime = DateTime.Now, CityTo = "Sofia", AddressTo = "bul Slivnica 156a", ArrivalTime = DateTime.Now, FreeSeatsCount = 4, Price = 5}
            );


        }


        public DbSet<User> Users { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<UserTravel> UserTravels { get; set; }
        public DbSet<PendingInvite> PendingInvites { get; set; }
    }
}
