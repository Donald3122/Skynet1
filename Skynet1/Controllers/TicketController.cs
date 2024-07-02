using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skynet1.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Skynet1.Controllers
{
    public class TicketController : Controller
    {
        private readonly Skynet1Context _context;

        public TicketController(Skynet1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string status)
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "Unknown";
            ViewBag.UserRole = userRole;
            var tickets = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(status) && status != "Все")
            {
                tickets = tickets.Where(t => t.Status == status);
            }

            var viewModel = new TicketFilterViewModel
            {
                Tickets = await tickets.ToListAsync(),
                CurrentStatus = status
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "Unknown";
            ViewBag.UserRole = userRole;
            var services = await _context.Services.ToListAsync();
            var viewModel = new TicketViewModel
            {
                Services = services
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel viewModel)
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "Unknown";
            ViewBag.UserRole = userRole;
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    NameTicket = viewModel.NameTicket,
                    Address = viewModel.Address,
                    Phone = viewModel.Phone,
                    Status = viewModel.Status,
                    ServiceId = viewModel.ServiceId
                };

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            viewModel.Services = await _context.Services.ToListAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                ticket.Status = "Одобрено";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                ticket.Status = "Отклонено";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Метод для поиска заявки по номеру телефона
        public async Task<IActionResult> SearchByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Phone == phone);

                if (ticket == null)
                {
                    return NotFound();
                }

                return Json(ticket);
            }
            catch (Exception ex)
            {
                // В случае ошибки вернуть соответствующий статус
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // Метод для отображения данных заявки по номеру телефона
        public IActionResult ShowTicket(Ticket ticket)
        {
            return View(ticket);
        }

        // Новый метод для отображения одобренных заявок
        public async Task<IActionResult> ApprovedTickets()
        {
            var userRole = HttpContext.Session.GetString("UserRole") ?? "Unknown";
            ViewBag.UserRole = userRole;
            var approvedTickets = await _context.Tickets
                .Where(t => t.Status == "Одобрено" || t.Status == "Выполняется" || t.Status == "Выполнено")
                .ToListAsync();

            var viewModel = new TicketFilterViewModel
            {
                Tickets = approvedTickets,
                CurrentStatus = "Одобрено"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StartWork(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                ticket.Status = "Выполняется";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ApprovedTickets));
        }

        [HttpPost]
        public async Task<IActionResult> CompleteWork(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                ticket.Status = "Выполнено";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ApprovedTickets));
        }
    }


}
