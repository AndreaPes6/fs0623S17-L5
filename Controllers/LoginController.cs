using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HotelSoloRicchi.Models;

namespace HotelSoloRicchi.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("BackOffice");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            string connString = ConfigurationManager.ConnectionStrings["HotelSoloRicchiDB"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @username AND Password = @password", conn);
            command.Parameters.AddWithValue("@username", admin.Username);
            command.Parameters.AddWithValue("@password", admin.Password);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["Id"].ToString(), true);
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult BackOffice()
        {
            var Id = HttpContext.User.Identity.Name;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");

        }


    }
}