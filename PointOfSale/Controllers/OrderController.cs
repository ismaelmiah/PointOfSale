using System;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Services;

namespace PointOfSale.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderServices _orderServices;
        public OrderController()
        {
            _orderServices = Startup.AutofacContainer.Resolve<OrderServices>();
        }

        public IActionResult GetAllData()
        {
            var allOrders = _orderServices.GetAllRecords();
            return Json(new { data = allOrders });
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            _orderServices.SaleProduct(productViewModel);
            return RedirectToAction("Data", "Product");
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return Json(_orderServices.DeleteRecord(id)
                ? new {success = true, message = "Delete Operation Successfully"}
                : new {success = false, message = "Record Not Available, Check Database"});
        }
    }
}
