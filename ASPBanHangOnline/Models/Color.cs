using System.ComponentModel.DataAnnotations;

namespace ASPBanHangOnline.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        [StringLength(300)]
        public string? ColorName { get; set; }

    }
}
