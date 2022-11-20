using System.ComponentModel.DataAnnotations;
using TripBooking.Core.Enums;

namespace TripBooking.Data.Reservations.Model
{
    internal class TripRegistration
    {
        [Key]
        public Guid Id { get; set; }
        public RegistrationStatus Status { get; set; }
        public string TripName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
