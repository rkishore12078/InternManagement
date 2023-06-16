using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LogsAPI.Models
{
    public class LogContext:DbContext
    {
        public LogContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<logs> Logs { get; set; }
    }
}
