using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RegistrationMVC.Controllers
{
    public class AddUserController : Controller
    {
        [HttpPost]
        public JsonResult insertdetail(UserModel user)
        {
            try
            {
                DbConnect d3 = new DbConnect();
                d3.Connect();
                MD5 md5 = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(user.Password + "abcd");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                String password = Convert.ToHexString(hashBytes);
                SqlCommand cmd = new SqlCommand("insert into tbl_user_credential (username,password,salt,role) values (@username,@password,@salt,@role);", d3.con);
                cmd.Parameters.AddWithValue("@username", user.Name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@salt", "abcd");
                cmd.Parameters.AddWithValue("@role", user.Role);
                int num = cmd.ExecuteNonQuery();
                if (num > 0)
                {

                    Response.Redirect("/Permission/ExecutePermission/?permissionid=4");
                    return Json(num);
                }
                else
                {
                    Response.Redirect("/Home/DashBoard");
                    return Json(num);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
