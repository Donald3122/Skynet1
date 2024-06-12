using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skynet1.Models
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }

        [Required]
        public string? NameTicket { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string Status { get; set; } = "В обработке"; // Default value

        [Required]
        public int? ServiceId { get; set; }

        public IEnumerable<Service> Services { get; set; } = new List<Service>();
    }
}
