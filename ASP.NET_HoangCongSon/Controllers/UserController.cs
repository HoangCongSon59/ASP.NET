using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_HoangCongSon.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Câu lệnh SQL kiểm tra thông tin đăng nhập
                string query = "SELECT IsAdmin FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số để tránh SQL Injection
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null) // Đăng nhập thành công
                    {
                        bool isAdmin = (bool)result;
                        if (isAdmin)
                        {
                            return RedirectToAction("Dashboard", "Admin"); // Trang Admin
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home"); // Trang người dùng
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng!";
                    }
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}