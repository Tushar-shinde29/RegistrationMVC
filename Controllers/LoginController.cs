using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RegistrationMVC.Controllers
{
    public class LoginController : Controller
    {
        



        public JsonResult AuthUser(string username, string password)
        {
            DbConnect d1 = new DbConnect();

            username = HttpContext.Request.Query["username"];
            password = HttpContext.Request.Query["password"];
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password + "abcd");
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            password = Convert.ToHexString(hashBytes);
            try
            {
                d1.Connect();
                SqlCommand cmd = new SqlCommand("select * from tbl_user_credential where username=@username AND password=@password AND is_active=1", d1.con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                UserModel user = new UserModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["userid"].ToString());
                        user.Name = reader["username"].ToString();
                        user.Role = Convert.ToInt32(reader["role"].ToString());
                        user.is_active = true;
                        HomeController.roleid = Convert.ToInt32(reader["role"].ToString());
                        HomeController.userid = Convert.ToInt32(reader["userid"].ToString());
                    }
                    d1.con.Close();

                    return Json(user);
                }
                else
                {
                    return Json(user);
                }

            }
            catch (Exception e)
            {
               
                throw;
            }
        }

        [HttpGet("")]
        [HttpGet("/Login")]
        [HttpGet("Home/Login")]
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}
