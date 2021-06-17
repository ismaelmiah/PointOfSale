using System;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Web.Models;

namespace PointOfSale.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetAllData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new ProductModel();
            var data = model.GetAllProducts(tableModel);
            return Json(data);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(Guid? id)
        {
            var model = new ProductModel();

            if (id == null)
                return PartialView(model);

            model = model.BuildEditProductModel(id.GetValueOrDefault());
            if (model == null)
                return NotFound();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == new Guid())
                {
                    //Create
                    model.SaveProduct(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Update
                    model.UpdateProduct(model.Id, model);
                    return RedirectToAction(nameof(Index));
                }
            }

            return PartialView(model);
        }
        
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var model = new ProductModel();
            var IsSucceed = model.DeleteProduct(id);
            return (Json(IsSucceed
            ? new { success = true, message = "Product Deleted Successfully"}
            : new { success = false, message = "Product Not Found!" }));
        }
    }
}
