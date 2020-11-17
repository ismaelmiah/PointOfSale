using System;
using System.Globalization;
using DataSets.Entity;
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
            //var OrderWithCategory = _uow.Category.Get()
            return Json(new { data = allOrders });
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            var product = _uow.Product.GetFirstOrDefault(x => x.Id == id & x.Quantity>0);
            if (product == null) return Json(new { success = false, message = "Product Stock Out" });

            var sale = new OrderDetails()
            {
                Count = 1,
                Price = product.SalePrice,
                Product = product,
                SaleDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };
            _uow.OrderDetails.Add(sale);
            _uow.Save();
            
            product.Quantity -= 1;
            _uow.Product.Update(product);
            _uow.Save();
            return Json(new { success = true, message = "Sale Operation Successfully" });

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var record = _uow.OrderDetails.Get(id);
            if(record == null ) return Json(new { success = false, message = "Record Not Available, Check Database" });
            _uow.OrderDetails.Remove(id);
            _uow.Save();
            var modifiedProduct = _uow.Product.Get(record.ProductId);
            modifiedProduct.Quantity += 1;
            _uow.Product.Update(modifiedProduct);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }
    }
}
