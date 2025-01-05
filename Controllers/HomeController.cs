using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NishuPortFolio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;



using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace NishuPortFolio.Controllers
{
    public class HomeController : Controller
    {
        // Inject the IConfiguration interface into your controller to access the connection string from appsettings.json file

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public SqlConnection conn;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        
                string connectionString = _configuration.GetConnectionString("MyConnectionString");
             conn=new SqlConnection( connectionString);
        }





        public IActionResult Index()
        {
            ViewData["ActiveMenu"] = "Home";
                   

            return View();
        }
        public IActionResult ProjectDetails()
        {
            ViewData["ActiveMenu"] = "Projects";

            return View();

        }
        public ActionResult Contact()
        {
            ViewData["ActiveMenu"] = "Contact";
            return View();

        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            ViewData["ActiveMenu"] = "Contact";
        
            string query = "insert into tblcontact(SName,Message, Email, Phone)values(@Name,@Message, @Email, @Phone)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", contact.SName);
            cmd.Parameters.AddWithValue("@Message", contact.Message);
            cmd.Parameters.AddWithValue("@Email", contact.Email);
            cmd.Parameters.AddWithValue("@Phone", contact.Phone);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            string msg = "";
            msg = "Saved Success";
            ViewBag.Msg = msg;
    
            return View();

        }

        public IActionResult Research()
        {
            ViewData["ActiveMenu"] = "Research";
            return View();
        }
        public IActionResult Projects()
        {
            ViewData["ActiveMenu"] = "Projects";
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
