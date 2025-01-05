using Microsoft.AspNetCore.Mvc;

namespace NishuPortFolio.Controllers.Admin
{
    public class AdminController : Controller
    {
      
        public IActionResult Index()
        {

            // Check if the cookie exists
            if (Request.Cookies.ContainsKey("userid"))
            {
                // Return Dashboard view if the cookie exists
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                // Return Login view if the cookie does not exist
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult Dashboard()
        {

            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

    }
 
}
