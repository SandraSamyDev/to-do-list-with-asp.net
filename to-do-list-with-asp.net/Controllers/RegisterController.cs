

using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Models;
using to_do_list_with_asp.net_.Data; 

namespace to_do_list_with_asp.net_.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(model);
                }

                var newUser = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }
    }
}