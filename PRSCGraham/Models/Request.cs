using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCGraham.Models
{
    [Table ("Request")]
    public class Request
    {
        [Key]
        public int Id { get; set; } 
        public int UserId { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Justification { get; set; } 
        public DateTime DateNeeded { get; set; }
        [StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";
        [StringLength(25)]
        public string Status { get; set; } = "NEW";
        
        public decimal Total { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public string? ReasonForRejection { get; set; }
        public User? User { get; set; }
             
    }
}
