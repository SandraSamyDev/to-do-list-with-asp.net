using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Models;

namespace to_do_list_with_asp.net_.Controllers
{
    public class RegisterController : Controller
    {
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
                HttpContext.Session.SetString("UserEmail", model.Email);
                HttpContext.Session.SetString("UserPassword", model.Password);
                HttpContext.Session.SetString("UserName", model.Username);

                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }
    }
}