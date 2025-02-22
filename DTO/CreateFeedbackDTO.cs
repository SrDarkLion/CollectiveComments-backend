using System.ComponentModel.DataAnnotations;

namespace CollectiveComments.DTO
{
    public class CreateFeedbackDTO
    {
        [Required(ErrorMessage = "The company code is mandatory.")]
        [StringLength(40, MinimumLength = 0, ErrorMessage = "The company code must have a maximum of 40 characters.")]
        public string CompanyCode { get; set; }

        [Required(ErrorMessage = "The feedback message is mandatory.")]
        [StringLength(600, MinimumLength = 0, ErrorMessage = "Feedback must be a maximum of 600 characters.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Feedback type is mandatory.")]
        public FeedbackType Type { get; set; }
    }
}
