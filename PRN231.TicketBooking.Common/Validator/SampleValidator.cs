using FluentValidation;

namespace PRN231.TicketBooking.Common.Validator
{
    public class SampleValidator : AbstractValidator<object>
    {
        public SampleValidator()
        {
            //RuleFor(x => x.AccountId).NotNull().NotEmpty().WithMessage("The accountId must be required!");
            //RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("The content must be required!");
            //RuleFor(x => x.ImageUrl).NotEmpty().NotNull().WithMessage("The file must be required!");
        }
    }
}