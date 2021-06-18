using System;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Web.Models;

namespace PointOfSale.Web.Controllers
{
    public class SaleDetailController : Controller
    {
        public IActionResult GetAllData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new SaleDetailModel();
            var data = model.GetAllSaleDetails(tableModel);
            return Json(data);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(Guid? id)
        {
            var model = new SaleDetailModel();

            if (id == null)
                return PartialView(model);

            model = model.BuildEditSaleDetailModel(id.GetValueOrDefault());
            if (model == null)
                return NotFound();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SaleDetailModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == new Guid())
                {
                    //Create
                    model.SaveSaleDetail(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Update
                    model.UpdateSaleDetail(model.Id, model);
                    return RedirectToAction(nameof(Index));
                }
            }

            return PartialView(model);
        }
        
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var model = new SaleDetailModel();
            var IsSucceed = model.DeleteSaleDetail(id);
            return (Json(IsSucceed
            ? new { success = true, message = "SaleDetail Deleted Successfully"}
            : new { success = false, message = "SaleDetail Not Found!" }));
        }
    }
}
