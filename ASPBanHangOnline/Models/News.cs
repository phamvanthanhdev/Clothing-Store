using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBanHangOnline.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        [Required]
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        [StringLength(10000)]
        public string? Contents { get; set; }
        [Required]
        [StringLength(1000)]
        public string? NewsPhoto { get; set; }
        public int TotalLike { get; set; } = 0;
        public int TotalComment { get; set; } = 0;
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin? Admin { get; set; }
        public bool IsNew { get; set; }
        public bool IsDisplay { get; set; }
    }
}
