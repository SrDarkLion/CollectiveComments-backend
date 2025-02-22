using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CollectiveComments.Models
{
    [Table("companies")]
    [Index(nameof(Code), IsUnique = true)]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(80)]
        public string Code { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public void GenerateCode()
        {
            string name = Name.Split(' ')[0].ToLower();
            string id = Id.ToString().Split('-')[0];

            Code = $"{name}_{id}";
        }

    }
}