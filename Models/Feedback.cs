using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectiveComments.Models
{
    [Table("feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        public FeedbackType Type { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(80)]
        public string CompanyCode { get; set; }

        [ForeignKey("CompanyCode")]
        public virtual Company Company { get; set; }
    }
}
