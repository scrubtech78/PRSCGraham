using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCGraham.Models
{
    [Table ("RequestLines")]
    public class RequestLines

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RequestId { get; set; }

        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public Request? Request { get; set; }
        public Product? Product { get; set; }

    }
}
