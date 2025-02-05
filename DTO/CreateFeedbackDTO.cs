using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace CollectiveComments.DTO
{
    public class CreateFeedbackDTO
    {
        [Required(ErrorMessage = "O código da empresa é obrigatório.")]
        [StringLength(40, MinimumLength = 0, ErrorMessage = "O código da empresa deve ter no máximo 40 caracteres.")]
        public string CompanyCode { get; set; }

        [Required(ErrorMessage = "A mensagem do feedback é obrigatória.")]
        [StringLength(600, MinimumLength = 0, ErrorMessage = "O feedback deve ter no máximo 600 caracteres.")]
        public string Message { get; set; }
    }
}
