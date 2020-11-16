using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataSets.Interfaces;

namespace PointOfSale.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;

        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult GetAllData()
        {
            var allOrders = _uow.OrderDetails.GetAll(includeProperties: "Product");
            return Json(new { data = allOrders });
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public IActionResult Details(Guid id)
        {
            return View();
        }
        
        // POST: OrderController/Delete/5
        [HttpDelete]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }
    }
}
