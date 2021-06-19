using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckoutOrderCommanValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommanValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must no exist in 50 char");

            RuleFor(z => z.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required");

            RuleFor(p => p.TotalPrice)
               .NotEmpty().WithMessage("{TotalPrice} is required.")
               .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }

    }
}
