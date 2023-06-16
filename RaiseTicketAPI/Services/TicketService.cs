using RaiseTicketAPI.Interfaces;
using RaiseTicketAPI.Models;
using RaiseTicketAPI.Models.DTOs;

namespace RaiseTicketAPI.Services
{
    public class TicketService : IManageTicket
    {
        private readonly ITicket _ticketRepo;

        public TicketService(ITicket ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }
        public Task<List<Ticket>?> GetAll()
        {
            return _ticketRepo.GetAll();
        }

        public async Task<Ticket?> Get(int id)
        {
            return await _ticketRepo.Get(id);
        }

        public async Task<List<Ticket>?> GetIssuedTickets()
        {
            var tickets = await _ticketRepo.GetAll();
            if (tickets != null)
            {
                var issuedTickets = tickets.Where(t=>t.SolutionStatus==false).ToList();
                if (issuedTickets.Count>0)
                    return issuedTickets;
            }
            return null;
        }

        public async Task<List<Ticket>?> GetSolvedTickets()
        {
            var tickets = await _ticketRepo.GetAll();
            if (tickets != null)
            {
                var issuedTickets = tickets.Where(t => t.SolutionStatus == true).ToList();
                if (issuedTickets.Count>0)
                    return issuedTickets;
            }
            return null;
        }

        public async Task<Ticket?> RaiseTicket(Ticket ticket)
        {
            ticket.IssueDate=DateTime.Now;
            ticket.ResolvedDate = new DateTime();
            ticket.SolutionStatus = false;
            var myTicket=await _ticketRepo.Add(ticket);
            if (myTicket != null)
                return myTicket;
            return null;
        }

        public async Task<TicketDTO?> ResolveTicket(TicketDTO ticketDTO)
        {
            Ticket ticket = new Ticket();
            ticket.TicketNumber = ticketDTO.TicketNumber;
            ticket.AdminID = ticketDTO.AdminID;
            ticket.SolutionStatus = true;
            ticket.ResolvedDate = DateTime.Now;
            var myTicket=await _ticketRepo.Update(ticket);
            if (myTicket != null)
                return ticketDTO;
            return null;
        }

        public async Task<List<Ticket>?> GetSpecificInternTickets(Ticket ticket)
        {
            var tickets=await _ticketRepo.GetAll();
            if(tickets==null) return null;
            var myTickets=tickets.Where(t=>t.InternID==ticket.InternID);
            if(myTickets!=null) return myTickets.ToList();
            return null;
        }
    }
}
