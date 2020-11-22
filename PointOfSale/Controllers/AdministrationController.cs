using Autofac;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Services;

namespace PointOfSale.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly MonthlyDetailServices _monthDetails;
        public AdministrationController()
        {
            _monthDetails = Startup.AutofacContainer.Resolve<MonthlyDetailServices>();
        }
        public IActionResult Index()
        {
            var model = new MonthDetailViewModel()
            {
                MonthDetails = _monthDetails.GetAllMonthDetails(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Result(MonthDetailViewModel monthDetailView)
        {
            var model = _monthDetails.GetSpecificDetails(monthDetailView);
            return RedirectToAction("Index", new { montView = new MonthDetailViewModel(){MonthDetails = model}});
        }
    }
}
