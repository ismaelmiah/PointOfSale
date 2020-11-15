using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert()
        {
            var product = new ProductVm()
            {
                Product = new Product(),
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVm productvm)
        {
            if (!ModelState.IsValid) return View(productvm);
            _uow.Product.Add(productvm.Product);
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
