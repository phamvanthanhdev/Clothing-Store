using System.ComponentModel.DataAnnotations;

namespace ASPBanHangOnline.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Username { get; set; }
        [Required]
        [StringLength(100)]
        public string? Passwords { get; set; }
        [Required]
        [StringLength(200)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        [Required]
        [StringLength(20)]
        public string? Address { get; set; }
        [Required]
        [StringLength(20)]
        public string? AccountNumber { get; set; }
    }
}
