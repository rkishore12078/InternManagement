using System.ComponentModel.DataAnnotations;

namespace RaiseTicketAPI.Models
{
    public class Ticket
    {
        [Key]
        public int TicketNumber { get; set; }
        public int InternID { get; set; }
        public int AdminID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool SolutionStatus { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ResolvedDate { get; set; }
    }
}
