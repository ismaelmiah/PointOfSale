using System;
using Autofac;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Create(Guid id, int quantity)
        {
            return Json(_orderServices.SaleProduct(id,quantity) 
                ? new { success = true, message = "Sale Operation Successfully" } 
                : new { success = false, message = "Product Stock Out" });
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
