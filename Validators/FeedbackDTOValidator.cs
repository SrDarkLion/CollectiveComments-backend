using FluentValidation;
using CollectiveComments.DTO;

namespace CollectiveComments.Validators
{
    public class FeedbackDTOValidator : AbstractValidator<CreateFeedbackDTO>
    {
        public FeedbackDTOValidator()
        {
            RuleFor(f => f.CompanyCode)
                .NotEmpty().WithMessage("O código da empresa é obrigatório.")
                .MinimumLength(0)
                .MaximumLength(40).WithMessage("O código da empresa pode ter no máximo 40 caracteres.");

            RuleFor(f => f.Message)
                .NotEmpty().WithMessage("A mensagem do feedback é obrigatória.")
                .MinimumLength(0)
                .MaximumLength(600).WithMessage("O feedback pode ter no máximo 600 caracteres.");
        }
    }
}
