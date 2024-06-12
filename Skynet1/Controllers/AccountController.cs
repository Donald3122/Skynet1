using Microsoft.AspNetCore.Mvc;
using Skynet1.Models;
using System.Linq;

namespace Skynet1.Controllers
{
    public class AccountController : Controller
    {
        private readonly Skynet1Context _context;

        public AccountController(Skynet1Context context)
        {
            _context = context;
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (AuthenticateUser(model))
                {
                    TempData["Message"] = "Login successful!";
                    TempData["AlertClass"] = "alert-success";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Invalid username or password!";
                    TempData["AlertClass"] = "alert-danger";
                }
            }

            return View(model);
        }

        private bool AuthenticateUser(LoginViewModel model)
        {
            var user = _context.Employees.FirstOrDefault(u => u.NameEmployee == model.Username && u.Password == model.Password);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
