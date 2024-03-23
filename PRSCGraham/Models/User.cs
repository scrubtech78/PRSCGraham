using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCGraham.Models
{
    [Table ("User")]
        public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength (10)]
        public string Password { get; set; } 
        //Encrypt 
        [StringLength(20)]
        public string Firstname { get; set; }

        [StringLength(20)]
        public string Lastname { get; set; }
        public string? Phone { get; set; }
        
        public string? Email { get; set; }
        public bool Reviewer { get; set; }
        public bool Admin { get; set; }
        public List<Request>? Requests { get; set; }

    }
}