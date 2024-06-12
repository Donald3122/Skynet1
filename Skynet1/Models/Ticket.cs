using System.ComponentModel.DataAnnotations;

namespace Skynet1.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public string? NameTicket { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public int? ServiceId { get; set; }
    }
}
