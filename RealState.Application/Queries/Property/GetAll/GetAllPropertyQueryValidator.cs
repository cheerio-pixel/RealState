using FluentValidation;

namespace RealState.Application.Queries.Property.GetAll
{
    public class GetAllPropertyQueryValidator
    : AbstractValidator<GetAllPropertyQuery>
    {
        public GetAllPropertyQueryValidator()
        {
            RuleFor(m => m.MinPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Mininum price cannot be negative");

            RuleFor(m => m.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Maxiumum price cannot be negative");

            When(m => m.MinPrice > 0 && m.MaxPrice > 0, () =>
                 {
                     RuleFor(x => x.MaxPrice)
                     .Must((m, x) => x > m.MinPrice)
                     .WithMessage("Maxmium price cannot be less than minium price.");
                 }
            );
        }
    }
}