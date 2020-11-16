using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult GetAllData()
        {
            var allProducts = _uow.Product.GetAll(includeProperties: "Category");
            return Json(new { data = allProducts });
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var product = new ProductVm()
            {
                Product = new Product(),
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(ProductVm productVm)
        {
            if (!ModelState.IsValid) return View(productVm);
            _uow.Product.Add(productVm.Product);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var model = _uow.Product.GetFirstOrDefault(x => x.Id == id);

            var product = new ProductVm()
            {
                Product = model,
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductVm productVm)
        {
            if (!ModelState.IsValid) return View(productVm);
            _uow.Product.Update(productVm.Product);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {
            var model = _uow.Product.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category");
            return View(model);
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var deleteData = _uow.Product.GetFirstOrDefault(x => x.Id == id);
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
            _uow.Product.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }
    }
}
