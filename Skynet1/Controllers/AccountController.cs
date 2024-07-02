using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var userRole = HttpContext.Session.GetString("UserRole") ?? "Unknown";
            ViewBag.UserRole = userRole;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AuthenticateUser(model);
                if (user != null)
                {
                    // Успешная аутентификация
                    SetUserSession(user); // Установка информации о пользователе и его роли в сессию
                    TempData["Message"] = "Вы успешно вошли в систему!";
                    TempData["AlertClass"] = "alert-success";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Неправильное имя пользователя или пароль! ";
                    TempData["AlertClass"] = "alert-danger";
                }
            }

            return View(model);
        }

        private Employee AuthenticateUser(LoginViewModel model)
        {
            // Загрузка пользователя с его ролью из базы данных
            var user = _context.Employee
                .Include(e => e.Role) // Подгрузка связанной сущности Role
                .FirstOrDefault(u => u.NameEmployee == model.Username && u.Password == model.Password);

            return user;
        }

        private void SetUserSession(Employee user)
        {
            // Установка информации о пользователе в сессию
            HttpContext.Session.SetInt32("UserId", user.EmployeeId);
            HttpContext.Session.SetString("UserName", user.NameEmployee);

            // Проверка наличия роли и установка её в сессию
            if (user.Role != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role.RoleName);
            }
            else
            {
                HttpContext.Session.SetString("UserRole", "Unknown");
            }
        }
    }
}
