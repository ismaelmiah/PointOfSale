using DataSets.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel categoryvm)
        {
            if (!ModelState.IsValid) return View(categoryvm);
            _uow.Category.Add(categoryvm.category);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit()
        {
            return View();

        }
        public IActionResult Delete()
        {
            return View();
        }

    }
}
