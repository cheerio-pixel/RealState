using FluentValidation;

namespace RealState.Application.Commands.Property.Create
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The Name field is required.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The Description field is required.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The Price must be greater than zero.");
            RuleFor(x => x.Rooms)
                .GreaterThan(0).WithMessage("The Rooms must be greater than zero.");
            RuleFor(x => x.Bathrooms)
                .GreaterThan(0).WithMessage("The Bathrooms must be greater than zero.");
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("The Code field is required.");
            RuleFor(x => x.Meters)
                .GreaterThan(0).WithMessage("The Meters must be greater than zero.");
            RuleFor(x => x.Pictures)
                .NotEmpty().WithMessage("The Pictures field is required.");
            RuleFor(x => x.PropertyTypeId)
                .NotEmpty().WithMessage("The PropertyTypeId field is required.");
            RuleFor(x => x.SalesTypeId)
                .NotEmpty().WithMessage("The SalesTypeId field is required.");
            RuleFor(x => x.UpgradeId)
                .NotEmpty().WithMessage("The UpgradeId field is required.");

        }
    }
}
