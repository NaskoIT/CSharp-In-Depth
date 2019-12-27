using Microsoft.AspNetCore.Mvc;
using WebAppExpressionTrees.Infrastructure;

namespace WebAppExpressionTrees.Controllers
{
    public class RedirectController : Controller
    {
        public IActionResult Privacy()
        {
            int id = 10;
            //instead of write magic strings like in this example: return RedirectToAction(nameof(HomeController.Privacy), "Home");
            return this.RedirectTo<HomeController>(c => c.Privacy(id, "some query"));
        }
    }
}
