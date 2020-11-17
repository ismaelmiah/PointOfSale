using System;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Services;

namespace PointOfSale.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductServices _productServices;
        public ProductController()
        {
            _productServices = Startup.AutofacContainer.Resolve<ProductServices>();
        }
        public IActionResult GetAllData()
        {
            var allProducts = _productServices.GetAllProducts();
            return Json(new { data = allProducts });
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(_productServices.CreateGet());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVm productVm)
        {
            if (!ModelState.IsValid) return View(productVm);
            _productServices.CreatePost(productVm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            return _productServices.EditProductGet(id) != null 
                ? (IActionResult) View(_productServices.EditProductGet(id)) 
                : NotFound(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVm productVm)
        {
            if (!ModelState.IsValid) return View(productVm);
            _productServices.EditProductPost(productVm);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {
            return _productServices.DetailsProduct(id) != null
                ? (IActionResult)View(_productServices.DetailsProduct(id))
                : NotFound(id);
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return Json(_productServices.DeleteProduct(id) 
            ? new { success = true, message = "Delete Operation Successfully"}
            : new { success = false, message = "Data Not Found!" });
        }
    }
}
