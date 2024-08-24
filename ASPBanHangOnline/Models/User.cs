using System.ComponentModel.DataAnnotations;

namespace ASPBanHangOnline.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string? UserEmail { get; set; }
        [Required]
        [StringLength(100)]
        public string? Passwords { get; set; }
        [Required]
        [StringLength(200)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(20)]
        public string? Phone { get; set; }
        public int Sex { get; set; }
    }
}
