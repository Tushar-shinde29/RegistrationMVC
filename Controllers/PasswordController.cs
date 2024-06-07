using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RegistrationMVC.Controllers
{
    public class PasswordController : Controller
    {
        public void Changepassword(IFormCollection formdata)
        {
            try
            {
                String ?password = formdata["password"].ToString();
                Console.WriteLine(password);    
                MD5 md5 = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password + "abcd");
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                password = Convert.ToHexString(hashBytes);
                Console.WriteLine(password);
                DbConnect d1 = new DbConnect();
                d1.Connect();
                SqlCommand cmd= new SqlCommand("update tbl_user_credential set password=@password where userid=@userid And is_active=1",d1.con);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@userid", HomeController.userid);
                int num =cmd.ExecuteNonQuery();
                if (num > 0)
                {
                    Response.Redirect("/");
                }
                else {
                    
                }






            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
