using System.ComponentModel.DataAnnotations;

namespace CollectiveComments.DTO
{
    public class CreateCompanyDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 15 characters.")]
        public string Password { get; set; }
    }
}