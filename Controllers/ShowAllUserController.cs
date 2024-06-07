using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class ShowAllUserController : Controller
    {
        public IActionResult ShowUsers()
        {
            try
            {
                DbConnect d1=new DbConnect();
                d1.Connect();
                List<Object> users = new List<Object>();
                
                SqlCommand cmd=new SqlCommand("exec show_users @role",d1.con);
                cmd.Parameters.AddWithValue("@role",HomeController.roleid);
                SqlDataReader reader=cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read()) {

                        var user = new {
                            Name = reader["username"].ToString(),
                            Id = Convert.ToInt32(reader["userid"].ToString()),
                            Role = reader["role_name"].ToString(),
                            FirstName = reader["firstname"].ToString(),
                            LastName = reader["lastname"].ToString(),
                            roleid= Convert.ToInt32(reader["role"].ToString()),
                            };
                        users.Add(user);

                    }
                    ViewBag.Users = users;
                    

                    reader.Close();
                    return View("AllUser");
                }
                else
                {
                    Console.WriteLine("in else");
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
