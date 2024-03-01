using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0205.Models;

namespace Mission08_Team0205.Controllers
{
    public class HomeController : Controller
    {
        private TasksContext _taskRepository;
/*        private Category _categoryRepository;*/

        public HomeController(TasksContext taskRepository)
        {
            _taskRepository = taskRepository;
/*            _categoryRepository = categoryRepository;*/
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Task()
        {
            ViewBag.Categories = _taskRepository.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Task(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Tasks.Add(task);
                _taskRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = _taskRepository.Categories.ToList();
                return View(task);
            }
        }

        public IActionResult Quadrants()
        {
            var tasks = _taskRepository.Tasks.Include(x => x.Category)
                .ToList();
            return View(tasks);
        }

/*        [HttpPost]
        public IActionResult MarkCompleted(int taskId)
        {
            _taskRepository.MarkTaskCompleted(taskId);
            return RedirectToAction("Quadrants");
        }*/

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.Tasks.Single(x => x.TaskId == id);
            ViewBag.Categories = _taskRepository.Categories.ToList();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(Models.Task task)
        {
                _taskRepository.Update(task);
                _taskRepository.SaveChanges();
                return RedirectToAction("Quadrants");
        }

        [HttpPost]
        public IActionResult Delete(Models.Task task)
        {
            _taskRepository.Tasks.Remove(task);
            return RedirectToAction("Quadrants");
        }
    }
}
