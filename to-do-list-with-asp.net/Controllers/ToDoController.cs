
using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Data;
using to_do_list_with_asp.net_.Models;
using System.Linq;

namespace to_do_list_with_asp.net_.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("UserName") != null;
        }

        private int? GetUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }


        public IActionResult Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Login");

            var userId = GetUserId();

            var tasks = _context.TodoTasks
                .Where(t => !t.IsCompleted && t.UserId == userId)
                .ToList();

            return View(tasks);
        }


        public IActionResult AllTasks()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Login");

            var userId = GetUserId();

            var tasks = _context.TodoTasks
                .Where(t => t.UserId == userId)
                .ToList();

            return View("Index", tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Login");

            var userId = GetUserId();

            if (!string.IsNullOrEmpty(title))
            {
                _context.TodoTasks.Add(new todotask
                {
                    Title = title,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false,
                    IsImportant = false,
                    UserId = userId.Value
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult CompletedTasks()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Login");

            var userId = GetUserId();

            var tasks = _context.TodoTasks
                .Where(t => t.IsCompleted && t.UserId == userId)
                .ToList();

            return View("Index", tasks);
        }

        public IActionResult Important()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Login");

            var userId = GetUserId();

            var tasks = _context.TodoTasks
                .Where(t => t.IsImportant && t.UserId == userId)
                .ToList();

            return View("Index", tasks);
        }


        public IActionResult ToggleComplete(int id, string returnUrl)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _context.SaveChanges();
            }

            return !string.IsNullOrEmpty(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index");
        }
        public IActionResult ToggleImportant(int id, string returnUrl)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                task.IsImportant = !task.IsImportant;
                _context.SaveChanges();
            }

            return !string.IsNullOrEmpty(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index");
        }

        public IActionResult Delete(int id, string returnUrl)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                _context.TodoTasks.Remove(task);
                _context.SaveChanges();
            }

            return !string.IsNullOrEmpty(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(todotask task)
        {
            var existingTask = _context.TodoTasks.FirstOrDefault(t => t.Id == task.Id);

            if (existingTask == null)
                return NotFound();

            existingTask.Title = task.Title;
            existingTask.IsImportant = task.IsImportant;
            existingTask.IsCompleted = task.IsCompleted;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}