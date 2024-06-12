using System;
using System.Collections.Generic;

namespace Skynet1.Models
{
    public partial class Service
    {
        public Service()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int ServiceId { get; set; }
        public string? NameService { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }  // Навигационное свойство
    }
}
