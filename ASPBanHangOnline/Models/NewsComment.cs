using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBanHangOnline.Models
{
    public class NewsComment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        [ForeignKey("News")]
        public int NewsId { get; set; }
        public News? News { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [StringLength(5000)]
        public string? Comments { get; set; }
    }
}
