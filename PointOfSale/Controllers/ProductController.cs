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
        [HttpGet]
        public IActionResult Data(Guid? id)
        {
            if (id == null)
            {
                return View(_productServices.CreateGet());
            }
            var model = _productServices.EditProductGet(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVm)
        {
            if (!ModelState.IsValid) return View(productVm);
            if (!productVm.Product.Id.Equals(default(Guid)))
            {
                _productServices.EditProductPost(productVm);
            }
            else _productServices.CreatePost(productVm);
            return RedirectToAction(nameof(Data));

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
            ? new { success = true, message = "Product Deleted Successfully"}
            : new { success = false, message = "Product Not Found!" });
        }
    }
}
