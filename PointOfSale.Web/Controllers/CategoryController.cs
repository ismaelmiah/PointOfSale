using System;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Web.Models;

namespace PointOfSale.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult GetAllData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new CategoryModel();
            var data = model.GetCategories(tableModel);
            return Json(data);
        }
        
        public IActionResult Upsert(Guid? id)
        {
            var model = new CategoryModel();

            if (id == null)
                return PartialView(model);

            model = model.BuildEditCategoryModel(id.GetValueOrDefault());
            if (model == null)
                return NotFound();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == new Guid())
                {
                    //Create
                    model.SaveCategory(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Update
                    model.UpdateCategory(model.Id, model);
                    return RedirectToAction(nameof(Index));
                }
            }

            return PartialView(model);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var model = new CategoryModel();
            var IsSucceed = model.DeleteCategory(id);
            return (Json(IsSucceed
                ? new { success = true, message = "Delete Operation Successfully" }
                : new { success = false, message = "Category Delete Problem" }));
        }
    }
}
