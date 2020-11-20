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
        public IActionResult Data(Guid? id)
        {
            if (id == null)
            {
                return View(_categoryServices.CreateCategoryGet());
            }
            var model = _categoryServices.EditCategoryGet(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            if (!categoryViewModel.Category.Id.Equals(default(Guid)))
            {
                //TODO: Updated Category must have others fields update value not null value
                _categoryServices.EditCategoryPost(categoryViewModel);
            }
            else _categoryServices.CreateCategoryPost(categoryViewModel);
            return RedirectToAction(nameof(Data));
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
                ? new { success = true, message = "Delete Operation Successfully" }
                : new { success = false, message = "Category Not Found! / Delete All Products" }));
        }
    }
}
