using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RaiseTicketAPI.Models
{
    public class TicketContext:DbContext
    {
        public TicketContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
