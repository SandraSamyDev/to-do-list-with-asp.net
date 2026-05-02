using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Data; // عشان يشوف الـ DbContext
using to_do_list_with_asp.net_.Models;
using System.Linq;

namespace to_do_list_with_asp.net_.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        // بنحقن الداتا بيز في الكنترولر
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
            // البحث عن اليوزر في جدول الـ Users باستخدام الإيميل والباسورد
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // لو لقيناه، بنحفظ اسمه في السيشن عشان يظهر في الموقع
                HttpContext.Session.SetString("UserName", user.Username);

                // تحويله لصفحة المهام
                return RedirectToAction("Index", "ToDo");
            }

            // لو البيانات غلط، بنرجع رسالة خطأ للـ View
            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }
    }
}