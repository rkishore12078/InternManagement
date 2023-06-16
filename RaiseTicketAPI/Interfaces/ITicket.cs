using RaiseTicketAPI.Models;

namespace RaiseTicketAPI.Interfaces
{
    public interface ITicket
    {
        public Task<Ticket?> Add(Ticket item);
        public Task<Ticket?> Delete(int key);
        public Task<Ticket?> Get(int key);
        public Task<Ticket?> Update(Ticket item);
        public Task<List<Ticket>?> GetAll();
    }
}
