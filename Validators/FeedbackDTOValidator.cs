using FluentValidation;
using CollectiveComments.DTO;

namespace CollectiveComments.Validators
{
    public class FeedbackDTOValidator : AbstractValidator<CreateFeedbackDTO>
    {
        public FeedbackDTOValidator()
        {
            RuleFor(f => f.CompanyCode)
                .NotEmpty().WithMessage("The company code is mandatory.")
                .MinimumLength(0)
                .MaximumLength(40).WithMessage("The company code can have a maximum of 40 characters.");

            RuleFor(f => f.Message)
                .NotEmpty().WithMessage("The feedback message is mandatory.")
                .MinimumLength(0)
                .MaximumLength(600).WithMessage("Feedback can be a maximum of 600 characters.");
        }
    }
}