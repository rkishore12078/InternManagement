using System.ComponentModel.DataAnnotations;

namespace UserandInternAPI.Models.DTOs
{
    public class ChangePasswordDTO
    {
        public int UserID { get; set; }
        [Required]
        public string? NewPassword { get; set; }
    }
}
