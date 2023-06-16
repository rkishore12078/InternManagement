using System.ComponentModel.DataAnnotations;

namespace LogAPI.Models
{
    public class logs
    {
        [Key]
        public int LogID { get; set; }
        public int InterId { get; set; }
        public DateTime LogIn { get; set; }
        public DateTime LogOut { get; set; }
    }
}
