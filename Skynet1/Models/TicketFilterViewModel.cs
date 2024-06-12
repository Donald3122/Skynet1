using System.Collections.Generic;

namespace Skynet1.Models
{
    public class TicketFilterViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public string CurrentStatus { get; set; }
        public List<string> TicketStatuses { get; set; }
    }
}
