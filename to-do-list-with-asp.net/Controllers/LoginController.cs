using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Data; 
using to_do_list_with_asp.net_.Models;
using System.Linq;

namespace to_do_list_with_asp.net_.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Username);

                return RedirectToAction("Index", "ToDo");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }
    }
}