using FluentValidation;

namespace RealState.Application.Commands.Property.Create
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Rooms).NotEmpty();
            RuleFor(x => x.Bathrooms).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Meters).NotEmpty();
            RuleFor(x => x.Pictures).NotEmpty();
            RuleFor(x => x.PropertyTypeId).NotEmpty();
            RuleFor(x => x.SalesTypeId).NotEmpty();
            RuleFor(x => x.UpgradeId).NotEmpty();
        }
    }
}
