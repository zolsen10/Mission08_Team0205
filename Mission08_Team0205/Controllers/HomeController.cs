using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0205.Models;

namespace Mission08_Team0205.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Task()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Task(Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(task);
        }

        public IActionResult Quadrants()
        {
            var tasks = _taskRepository.GetIncompleteTasks();
            return View(tasks);
        }

        [HttpPost]
        public IActionResult MarkCompleted(int taskId)
        {
            _taskRepository.MarkTaskCompleted(taskId);
            return RedirectToAction("Quadrants");
        }

        [HttpGet]
        public IActionResult Edit(int taskId)
        {
            var task = _taskRepository.GetTaskById(taskId);
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.UpdateTask(task);
                return RedirectToAction("Quadrants");
            }

            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            _taskRepository.DeleteTask(taskId);
            return RedirectToAction("Quadrants");
        }
    }
}
