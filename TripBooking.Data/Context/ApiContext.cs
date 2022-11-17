using Microsoft.EntityFrameworkCore;
using TripBooking.Data.Trips.Model;

namespace TripBooking.Data.Context
{
    internal class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
    }
}
