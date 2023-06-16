using RaiseTicketAPI.Models;
using RaiseTicketAPI.Models.DTOs;

namespace RaiseTicketAPI.Interfaces
{
    public interface IManageTicket
    {
        public Task<Ticket?> RaiseTicket(Ticket ticket);
        public Task<List<Ticket>?> GetAll();
        public Task<TicketDTO?> ResolveTicket(TicketDTO ticketDTO);
        public Task<List<Ticket>?> GetSolvedTickets();
        public Task<List<Ticket>?> GetIssuedTickets();
        public Task<Ticket?> Get(int id);
        public Task<List<Ticket>?> GetSpecificInternTickets(Ticket ticket);

    }
}
