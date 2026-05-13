using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Data; 
using to_do_list_with_asp.net_.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

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
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("Index", "ToDo");
        }
    }
}