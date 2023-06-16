using Microsoft.EntityFrameworkCore;
using RaiseTicketAPI.Exceptions;
using RaiseTicketAPI.Interfaces;
using RaiseTicketAPI.Models;

namespace RaiseTicketAPI.Services
{
    public class TicketRepo : ITicket
    {
        private TicketContext _context;

        public TicketRepo(TicketContext context)
        {
            _context = context;
        }
        public async Task<Ticket?> Add(Ticket item)
        {
            try
            {
                _context.Tickets.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public Task<Ticket?> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket?> Get(int key)
        {
            try
            {
                var ticket = await _context.Tickets.SingleOrDefaultAsync(t=>t.TicketNumber==key);
                if(ticket!=null)
                    return ticket;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<List<Ticket>?> GetAll()
        {
            try
            {
                var tickets = await _context.Tickets.ToListAsync();
                if(tickets!=null)
                    return tickets;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<Ticket?> Update(Ticket item)
        {
            var ticket=await Get(item.TicketNumber);
            if (ticket != null)
            {
                ticket.Title = item.Title != null ? item.Title : ticket.Title;
                ticket.Description = item.Description != null ? item.Description : ticket.Description;
                ticket.AdminID = item.AdminID != 0 ? item.AdminID : ticket.AdminID;
                ticket.IssueDate = ticket.IssueDate;
                ticket.SolutionStatus = item.SolutionStatus ? item.SolutionStatus : ticket.SolutionStatus;
                ticket.ResolvedDate = item.ResolvedDate;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }
    }
}
