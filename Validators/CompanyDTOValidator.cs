using FluentValidation;
using CollectiveComments.DTO;

namespace CollectiveComments.Validators
{
    public class CompanyDTOValidator : AbstractValidator<CreateCompanyDTO>
    {
        public CompanyDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required!")
                .MinimumLength(3).WithMessage("The name must be between 3 characters.")
                .MaximumLength(50).WithMessage("The name can have a maximum of 100 characters.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("The name must be between 6 characters.")
                .MaximumLength(15).WithMessage("The name can have a maximum of 15 characters.");
        }
    }
}
