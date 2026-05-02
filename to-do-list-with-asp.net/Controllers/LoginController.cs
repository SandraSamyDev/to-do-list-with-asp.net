using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using to_do_list_with_asp.net_.Models;

namespace to_do_list_with_asp.net_.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var savedEmail = HttpContext.Session.GetString("UserEmail");
                var savedPassword = HttpContext.Session.GetString("UserPassword");

                if (model.Email == savedEmail && model.Password == savedPassword)
                {
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                   // edit
                    return RedirectToAction("Index", "ToDo");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password.");
                }
            }

            return View(model);
        }
      
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}