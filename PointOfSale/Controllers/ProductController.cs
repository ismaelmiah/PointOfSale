using Microsoft.AspNetCore.Mvc;

namespace PointOfSale.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
