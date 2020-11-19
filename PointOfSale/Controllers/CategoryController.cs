using System;
using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Services;

namespace PointOfSale.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryServices _categoryServices;
        public CategoryController()
        {
            _categoryServices = Startup.AutofacContainer.Resolve<CategoryServices>();
        }
        public IActionResult GetAllData()
        {
            var allCategories = _categoryServices.GetAllCategory();
            return Json(new { data = allCategories });
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
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            _categoryServices.CreateCategoryPost(categoryViewModel);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(Guid id)
        {
            return View(_categoryServices.EditCategoryGet(id));
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            _categoryServices.EditCategoryPost(categoryViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {
            var model = _categoryServices.DetailsCategory(id);
            return View(model);
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return (Json(_categoryServices.DeleteCategory(id)
                ? new {success = false, message = "Category Not Found!"}
                : new {success = true, message = "Delete Operation Successfully"}));
        }
    }
}
