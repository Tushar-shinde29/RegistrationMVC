using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RegistrationMVC.Controllers
{
    public class PermissionController : Controller
    {

        public IActionResult Index()
        {
            return View("DashBoard");
        }
        private readonly ILogger<PermissionController> _logger;


        public PermissionController(ILogger<PermissionController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddUser()
        {

            UserModel model = new UserModel();
            return View("AddUser", model);
        }

        public IActionResult AddDetails()
        {
            
            return View("Adddetail");
        }
        public IActionResult UpdateDetails()
        {
            UserDetail u1 = new UserDetail();
            try
            {
                DbConnect d1 = new DbConnect();
                d1.Connect();
                SqlCommand cmd = new SqlCommand("exec show_own_detail @userid", d1.con);
                cmd.Parameters.AddWithValue("@userid", HomeController.userid);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    u1.Id = HomeController.userid;
                    while (reader.Read())
                    {
                        u1.Fname = reader["firstname"].ToString();
                        u1.Lname = reader["lastname"].ToString();
                        u1.Phone = reader["mobileno"].ToString();
                        u1.Email = reader["email"].ToString();
                        u1.city = reader["city"].ToString();
                        u1.state = reader["state"].ToString();

                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

            return View("Adddetail", u1);
        }

        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }



        public IActionResult ExecutePermission()
        {
            int prid = Convert.ToInt32(HttpContext.Request.Query["permissionid"]);
            _logger.LogInformation(prid.ToString());
            switch (prid)
            {
                case 1:
                    return AddUser();
                case 2:
                    return UpdateDetails();
                case 3:
                    ShowAllUserController s3 = new ShowAllUserController();
                    return s3.ShowUsers();

                case 4:
                    ShowAllUserController s1 = new ShowAllUserController();
                    return s1.ShowUsers();
                case 5:
                    ShowOwnDetailController s2 = new ShowOwnDetailController();
                    return s2.Viewdetails();
                case 8:
                    ShowAllUserController s4 = new ShowAllUserController();
                    return s4.ShowUsers();

                case 6:
                    return ChangePassword();
                case 9:
                    return AddDetails();
                default:
                    return Index();
            }
        }


    }
}
