using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class ShowOwnDetailController : Controller
    {
        public IActionResult Viewdetails()
        {
            try
            {
                DbConnect d1 = new DbConnect();
                d1.Connect();
                List<Object> users = new List<Object>();
                SqlCommand cmd = new SqlCommand("exec show_own_detail @userid", d1.con);
                cmd.Parameters.AddWithValue("@userid", HomeController.userid);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var user = new
                        {
                            Name = reader["username"].ToString(),
                            FirstName = reader["firstname"].ToString(),
                            LastName = reader["lastname"].ToString(),
                            Mobileno = reader["mobileno"].ToString(),
                            Email = reader["email"].ToString(),
                            City = reader["city"].ToString(),
                            State = reader["state"].ToString()
                        };
                        users.Add(user);
                    }
                    ViewBag.Users = users;
                    return View("Viewdetail");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
