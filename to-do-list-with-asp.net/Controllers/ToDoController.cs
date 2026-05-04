
using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Models;
using to_do_list_with_asp.net_.Data; 
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            ViewData["TitleName"] = "My Day";
            ViewData["Icon"] = "bi-brightness-high";

            var pendingTasks = _context.TodoTasks.Where(t => !t.IsCompleted).ToList();
            return View(pendingTasks);
        }
        public IActionResult AllTasks()
        {
            ViewData["TitleName"] = "All Tasks";
            ViewData["Icon"] = "bi-house-door";
            var allTasks = _context.TodoTasks.ToList();
            return View("Index", allTasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var newTask = new todotask
                {
                    Title = title,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false,
                    IsImportant = false
                };

                _context.TodoTasks.Add(newTask);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult CompletedTasks()
        {
            ViewData["TitleName"] = "Completed Tasks";
            ViewData["Icon"] = "bi-check-all";
            var finishedTasks = _context.TodoTasks.Where(t => t.IsCompleted).ToList();
            return View("Index", finishedTasks);
        }

        public IActionResult Important()
        {
            ViewData["TitleName"] = "Important";
            ViewData["Icon"] = "bi-star text-primary";
            var importantTasks = _context.TodoTasks.Where(t => t.IsImportant).ToList();
            return View("Index", importantTasks);
        }

        public IActionResult ToggleComplete(int id)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _context.SaveChanges(); // لازم دي عشان التعديل يتسمع في SQL
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToggleImportant(int id)
        {
            var task = _context.TodoTasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsImportant = !task.IsImportant;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}