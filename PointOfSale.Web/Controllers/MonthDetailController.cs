using System;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Web.Models;

namespace PointOfSale.Web.Controllers
{
    public class MonthDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new MonthDetailModel();
            var data = model.GetAllMonthDetailModels(tableModel);
            return Json(data);
        }
        public IActionResult Upsert(Guid? id)
        {
            var model = new MonthDetailModel();

            if (id == null)
                return PartialView(model);

            model = model.BuildEditMonthDetailModel(id.GetValueOrDefault());
            if (model == null)
                return NotFound();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MonthDetailModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == new Guid())
                {
                    //Create
                    model.SaveMonthDetailModel(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Update
                    model.UpdateMonthDetailModel(model.Id, model);
                    return RedirectToAction(nameof(Index));
                }
            }

            return PartialView(model);
        }
        
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var model = new MonthDetailModel();
            var IsSucceed = model.DeleteMonthDetailModel(id);
            return (Json(IsSucceed
            ? new { success = true, message = "Month Detail Deleted Successfully"}
            : new { success = false, message = "Month Detail Not Found!" }));
        }
    }
}