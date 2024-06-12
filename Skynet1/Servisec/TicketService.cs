using System.Threading.Tasks;
using Skynet1.Models;

namespace Skynet1.Services
{
    public class TicketService
    {
        private readonly Skynet1Context _dbContext;

        public TicketService(Skynet1Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            _dbContext.Tickets.Add(ticket);
            await _dbContext.SaveChangesAsync();
        }
    }
}
