using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserandInternAPI.Models
{
    public class Intern
    {
        public Intern()
        {
            Name = string.Empty;
            Gender = "Unknown";
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public User? User { get; set; } = null;
        [MinLength(5, ErrorMessage = "Name cannot be less than 3 char")]
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender cannot be empty")]
        public string Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
