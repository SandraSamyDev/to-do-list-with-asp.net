using Microsoft.AspNetCore.Mvc;
using to_do_list_with_asp.net_.Models;
using System.Collections.Generic;
using System.Linq; 

namespace to_do_list_with_asp.net_.Controllers
{
    public class ToDoController : Controller
    {
        private static List<todotask> _tasks = new List<todotask>();

        public IActionResult Index()
        {
            ViewData["TitleName"] = "My Day";
            ViewData["Icon"] = "bi-brightness-high";

            var pendingTasks = _tasks.Where(t => !t.IsCompleted).ToList();
            return View(pendingTasks);
        }

        public IActionResult AllTasks()
        {
            ViewData["TitleName"] = "All Tasks";
            ViewData["Icon"] = "bi-house-door";
            return View("Index", _tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var newTask = new todotask
                {
                    Id = _tasks.Count + 1,
                    Title = title,
                    CreatedDate = DateTime.Now,
                    IsCompleted = false,
                    IsImportant = false
                };
                _tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        public IActionResult CompletedTasks()
        {
            ViewData["TitleName"] = "Completed Tasks";
            ViewData["Icon"] = "bi-check-all";
            var finishedTasks = _tasks.Where(t => t.IsCompleted).ToList();
            return View("Index", finishedTasks);
        }

        public IActionResult Important()
        {
            ViewData["TitleName"] = "Important";
            ViewData["Icon"] = "bi-star text-primary";
            var importantTasks = _tasks.Where(t => t.IsImportant).ToList();
            return View("Index", importantTasks);
        }

        
        public IActionResult ToggleComplete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted; 
            }
            return RedirectToAction("Index"); 
        }

        public IActionResult ToggleImportant(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsImportant = !task.IsImportant;
            }
            return RedirectToAction("Index");
        }
    }
}