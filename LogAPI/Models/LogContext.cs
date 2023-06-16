using Microsoft.EntityFrameworkCore;

namespace LogAPI.Models
{
    public class LogContext:DbContext
    {
        public LogContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<logs> Logs { get; set; }
    }
}
