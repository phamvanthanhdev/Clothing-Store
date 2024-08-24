using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBanHangOnline.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalPrice { get; set; }
        [Required]
        [StringLength(50)]
        public string? Phone { get; set; }
        [Required]
        [StringLength(5000)]
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public int Status { get; set; } = 0;
    }
}
