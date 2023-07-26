using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings
{
    public class InsertBookingValidation : AbstractValidator<InsertBookingDto>
    { 

        public InsertBookingValidation()
        {
            RuleFor(booking => booking.StartDate)
                .NotEmpty().WithMessage("Start Date can not be Null")
                .Must(WorkingHours).WithMessage("Must Be On Working Hours Between 09.00 to 17.00");
            RuleFor(booking => booking.EndDate)
                .NotEmpty().WithMessage("End Date can not be Null")
                .GreaterThanOrEqualTo(booking=>booking.StartDate.AddHours(1))
                .Must(WorkingHours).WithMessage("Must Be On Working Hours Between 09.00 to 17.00"); ;
            RuleFor(booking => booking.Status)
                .IsInEnum().WithMessage("Status can not be Null");
            RuleFor(booking => booking.Remarks)
                .NotEmpty().WithMessage("Remarks can not be Null");
            RuleFor(booking => booking.RoomGuid)
                .NotEmpty().WithMessage("Room Guid can not be Null");
            RuleFor(booking => booking.EmployeeGuid)
                .NotEmpty().WithMessage("Employee Guid can not be Null");
        }

        private bool WorkingHours(DateTime time)
        {

            var start = new TimeSpan(09, 00, 00);
            var end = new TimeSpan(17, 00, 00);
            TimeSpan currenttime = time.TimeOfDay;
            return  currenttime>=start && currenttime<=end;
        }
    }
}
