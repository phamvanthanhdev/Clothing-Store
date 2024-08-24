using System.ComponentModel.DataAnnotations;

namespace ASPBanHangOnline.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }
        [Required]
        [StringLength(10)]
        public string? SizeName { get; set; }

    }
}
