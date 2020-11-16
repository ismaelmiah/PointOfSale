using System;
using System.IO;
using System.Linq;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult GetAllData()
        {
            var allCategories = _uow.Category.GetAll(includeProperties: "Products");
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
            _uow.Category.Add(categoryViewModel.category);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(Guid id)
        {
            var model = _uow.Category.GetFirstOrDefault(x => x.Id == id);

            var categoryViewModel = new CategoryViewModel()
            {
                category = model
            };
            return View(categoryViewModel);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);
            _uow.Category.Update(categoryViewModel.category);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {
            var model = _uow.Category.GetFirstOrDefault(x => x.Id == id, includeProperties: "Products");
            return View(model);
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var deleteData = _uow.Category.GetFirstOrDefault(x => x.Id == id);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found!" });

            if (deleteData.ImageUrl != null)
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, deleteData.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _uow.Category.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }
    }
}
