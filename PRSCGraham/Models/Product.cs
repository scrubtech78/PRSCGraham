using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCGraham.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        [StringLength(150)]
        public  string PartNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set;}
        [Required]
        [StringLength(255)]
        public string Unit { get; set; }
        [StringLength(255)]
        public string? PhotoPath { get; set; }

        public Vendor? Vendor { get; set; }
    }
}
