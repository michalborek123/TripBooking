using Microsoft.EntityFrameworkCore;

namespace TripBooking.Data.Context
{
    internal class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }
    }
}
