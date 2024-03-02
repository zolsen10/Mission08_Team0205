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
            return View(new TaskModel());
        }

        [HttpPost]
        public IActionResult Task(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.Tasks.Add(task);
                _taskRepository.SaveChanges();
                return RedirectToAction("Task");
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
            return View("Task", task);
        }

        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
                _taskRepository.Update(task);
                _taskRepository.SaveChanges();
                return RedirectToAction("Quadrants");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _taskRepository.Tasks.Single(x => x.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(TaskModel task)
        {
            _taskRepository.Tasks.Remove(task);
            _taskRepository.SaveChanges();
            return RedirectToAction("Quadrants");
        }

        [HttpGet]
        public IActionResult MarkCompleted(int id)
        {
            var task = _taskRepository.Tasks.Single(x => x.TaskId == id);
            task.CompletedTask = true;
            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
            return RedirectToAction("Quadrants");
        }
    }
}
