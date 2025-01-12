using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


using Microsoft.Data.SqlClient;
using NishuPortFolio.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Numerics;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace NishuPortFolio.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public SqlConnection conn;
        public AdminController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            conn = new SqlConnection(connectionString);
        }

        public IActionResult Index()
        {
            ViewData["ActiveMenu"] = "Dashboard";

            // Check if the cookie exists

            var c = HttpContext.Session.GetString("Username");

              if (HttpContext.Session.GetString("Username") != null)
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

        // List Pages

        public IActionResult Dashboard()
        {
            ViewData["ActiveMenu"] = "Dashboard";
            return View();
        }
        public IActionResult Portfolio()

        {

            ViewData["ActiveMenu"] = "Portfolio";
            ViewBag.portfoliodata = GetPortFolio();

            return View();
        }


        public IActionResult Degree()

        {

            ViewData["ActiveMenu"] = "Degree";
            ViewBag.degreedata = GetDegree();

            return View();
        }

        //end List Pages


        //start Add Pages

        public IActionResult PortfolioAdd()
        {
            ViewData["ActiveMenu"] = "Portfolio";
            return View();
        }

        

                public IActionResult DegreeAdd()
        {
            ViewData["ActiveMenu"] = "Degree";
            return View();
        }

        // end Add Pages

        // Get List Data


        public List<PortFolioAdd> GetPortFolio()
        {
            ViewData["ActiveMenu"] = "Portfolio";
            using (var connection = conn)
            {
                string query = "SELECT * FROM [tblportfolio]";

                // Query the database and map the results to a list of Student objects
                var portfoliodata = connection.Query<PortFolioAdd>(query).ToList();
                return portfoliodata;


            }
        }


        public List<DegreeAdd> GetDegree()
        {
            ViewData["ActiveMenu"] = "Degree";
            using (var connection = conn)
            {
                string query = "SELECT * FROM [tbldegree]";

                // Query the database and map the results to a list of Student objects
                var degreedata = connection.Query<DegreeAdd>(query).ToList();
                return degreedata;


            }
        }

        // End Get List Data

        // Start Portfolioupdate
        

              public ActionResult UpdateDegree()
        {
            ViewData["ActiveMenu"] = "Degree";
            DegreeAdd degreeAdd = UpdateDegreeByID();

            if (degreeAdd == null)
            {
                return NotFound(); // Handle case where the student is not found
            }

            return View(degreeAdd); // Pass the student object to the view
        }
        public ActionResult UpdatePortfolio()
        {
            ViewData["ActiveMenu"] = "Portfolio";
            PortFolioAdd portFolioAdd = UpdatePortfolioByID();

            if (portFolioAdd == null)
            {
                return NotFound(); // Handle case where the student is not found
            }

            return View(portFolioAdd); // Pass the student object to the view
        }
        public ActionResult PortfolioUpdate(PortFolioAdd PortFolioAdd)
        {
            ViewData["ActiveMenu"] = "Portfolio";
            string query = "update tblportfolio set portfoliotitle=@portfoliotitle,portfoliodescription=@portfoliodescription where portfolioid=@portfolioid";


            using (var connection = conn)
            {
                connection.Open();
                var addprotfolio = new
                {
                    portfoliotitle = PortFolioAdd.portfoliotitle,
                    portfoliodescription = PortFolioAdd.portfoliodescription,
                    portfolioid= PortFolioAdd.portfolioid
                };
                connection.Close();
                int rowsAffected = connection.Execute(query, addprotfolio);
                ViewBag.portfoliodata = GetPortFolio();
                return View("Portfolio");



            }
        }

        

                 public ActionResult DegreeUpdate(DegreeAdd DegreeAdd)
        {
            ViewData["ActiveMenu"] = "Degree";
            string query = "update tbldegree set DegreeTitle=@DegreeTitle,DegreeInstitute=@DegreeInstitute where DegreeID=@DegreeID";


            using (var connection = conn)
            {
                connection.Open();
                var addprotfolio = new
                {
                    DegreeTitle = DegreeAdd.DegreeTitle,
                    DegreeInstitute = DegreeAdd.DegreeInstitute,
                    DegreeID = DegreeAdd.DegreeID
                };
                connection.Close();
                int rowsAffected = connection.Execute(query, addprotfolio);
                ViewBag.degreedata = GetDegree();
                return View("Degree");



            }
        }


        // End Portfolioupdate

        // start Delete code


        

             public IActionResult DeleteDegree(int id)
        {

            string query = "DELETE FROM tbldegree WHERE DegreeID = @DegreeID";

            using (var connection = conn)
            {
                conn.Open();
                int rowsAffected = conn.Execute(query, new { DegreeID = id });
                conn.Close();
            }

            return RedirectToAction("Degree");
        }


        [HttpPost]
        public IActionResult DeletePortfolio(int id)
        {

            string query = "DELETE FROM tblportfolio WHERE portfolioid = @portfolioid";

            using (var connection = conn)
            {
                conn.Open();
                int rowsAffected = conn.Execute(query, new { portfolioid = id });
                conn.Close();
            }

            return RedirectToAction("Portfolio");
        }

        // end delete code


        [HttpPost]


        // Start Save data

        

             public ActionResult DegreeSave(DegreeAdd DegreeAdd)
        {
            ViewData["ActiveMenu"] = "Degree";

            string query = "INSERT INTO tbldegree(DegreeTitle,DegreeInstitute) VALUES(@DegreeTitle,@DegreeInstitute)";


            using (var connection = conn)
            {
                connection.Open();
                var addprotfolio = new
                {
                    DegreeTitle = DegreeAdd.DegreeTitle,
                    DegreeInstitute = DegreeAdd.DegreeInstitute

                };
                connection.Close();
                int rowsAffected = connection.Execute(query, addprotfolio);
                ModelState.Clear();
                string msg = "";
                msg = "Save Data Successfully.";
                ViewBag.Msg = msg;
                return View("DegreeAdd");



            }



        }
        public ActionResult PortfolioSave(PortFolioAdd PortFolioAdd)
        {
            ViewData["ActiveMenu"] = "Portfolio";

            string query = "INSERT INTO tblportfolio(portfoliotitle,portfoliodescription) VALUES(@portfoliotitle,@portfoliodescription)";


            using (var connection = conn)
            {
                connection.Open();
                var addprotfolio = new
                {
                    portfoliotitle = PortFolioAdd.portfoliotitle,
                    portfoliodescription = PortFolioAdd.portfoliodescription

                };
                connection.Close();
                int rowsAffected = connection.Execute(query, addprotfolio);
                ModelState.Clear();
                string msg = "";
                msg = "Save Data Successfully.";
                ViewBag.Msg = msg;
                return View("PortfolioAdd");



            }


       
        }


        // End Save data


        // Login
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult checklogin(AdminModelLogin AdminModelLogin)
        {
            string query = "SELECT * FROM tbluser WHERE username = @Username AND userpassword = @Password";

            using (var connection = conn)
            {
                connection.Open();
                var user = connection.QueryFirstOrDefault(query, new
                {
                    Username = AdminModelLogin.username, // Ensure `Username` exists in AdminModelLogin
                    Password = AdminModelLogin.password  // Ensure `Password` exists in AdminModelLogin
                });
                connection.Close();
                if (user == null)
                {
                    string msg = "";
                    msg = "Login Failed";
                    ViewBag.Msg = msg;
                    return View("Login");
                }
                else {

                    string username = user.username;
                    Int32 userid= user.userid;
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetInt32("UserId", userid);


                }


            }
            

            return View("Dashboard");
        }


        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Optionally, remove the authentication cookie
            HttpContext.SignOutAsync();

            // Redirect to the login or home page
            return RedirectToAction("Login", "Admin");
        }


        

                    public DegreeAdd UpdateDegreeByID()
        {

            using (var connection = conn)
            {
                // Define the query with a parameter placeholder
                string query = "SELECT * FROM [tbldegree] WHERE [DegreeID] = @DegreeID";

                // Execute the query and map the result to the Student object


                var portfoliodata = connection.QueryFirstOrDefault<DegreeAdd>(query, new { DegreeID = Convert.ToInt32(HttpContext.Request.Query["sid"]) });

                return portfoliodata;




            }
        }

        public PortFolioAdd UpdatePortfolioByID()
        {
           
            using (var connection = conn)
            {
                // Define the query with a parameter placeholder
                string query = "SELECT * FROM [tblportfolio] WHERE [portfolioid] = @portfolioid";

                // Execute the query and map the result to the Student object
              

                var portfoliodata = connection.QueryFirstOrDefault<PortFolioAdd>(query, new { portfolioid = Convert.ToInt32(HttpContext.Request.Query["sid"]) });

                return portfoliodata;

    

    
            }
        }

        // end Login

 
   

    }
 
}
