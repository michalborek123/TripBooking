using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Reservations.Model;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Context
{
    internal class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripRegistration> TripRegistrations { get; set; }
    }
}
