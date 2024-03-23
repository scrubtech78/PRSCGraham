using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCGraham.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }  
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }
        [Required]
        [StringLength(75)]
        public string Email { get; set; }
        public Product? Product { get; set; }

    }
}
