using FluentValidation;
using TripBooking.Core.Reservations.Commands;

namespace TripBooking.Core.Reservations.Validations
{
    internal class TripRegistrationRequestValidator : AbstractValidator<TripRegistrationRequest>
    {
        public TripRegistrationRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.TripName).NotNull().NotEmpty();
        }
    }
}
