namespace RaiseTicketAPI.Models.DTOs
{
    public class TicketDTO
    {
        public int TicketNumber { get; set; }
        public int AdminID { get; set; }
        public DateTime ResolvedDate { get; set; }
    }
}
