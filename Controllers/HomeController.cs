using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistrationMVC.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;

namespace RegistrationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public static int roleid;
        public static int userid;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<Permission> GetPermission()
        {
            DbConnect d2 = new DbConnect();

            try
            {
                roleid = Convert.ToInt32(HttpContext.Request.Query["roleid"]);
                userid = Convert.ToInt32(HttpContext.Request.Query["userid"]);
                d2.Connect();
                SqlCommand cmd = new SqlCommand("select tbl_roles_has_permissions.permission as permissionid,tbl_permission.permsission from tbl_roles_has_permissions left join tbl_permission on tbl_roles_has_permissions.permission=tbl_permission.permission_id where tbl_roles_has_permissions.role_id=@role", d2.con);
                cmd.Parameters.AddWithValue("@role", Convert.ToInt32(HttpContext.Request.Query["roleid"]));
                SqlDataReader permissions = cmd.ExecuteReader();
                List<Permission> userpermission = new List<Permission>();
                if (permissions.HasRows)
                {
                    while (permissions.Read())
                    {
                        var permission1 = new Permission();
                        permission1.permissionId = Convert.ToInt32(permissions["permissionid"].ToString());
                        permission1.permissionName = permissions["permsission"].ToString();
                        userpermission.Add(permission1);
                    }
                }
                else
                {

                }
                permissions.Close();
                SqlCommand cmd1 = new SqlCommand("select tbl_user_has_special_permission.permission as permissionid," +
                    "tbl_permission.permsission from tbl_user_has_special_permission left join tbl_permission on " +
                    "tbl_user_has_special_permission.permission = tbl_permission.permission_id " +
                    "where tbl_user_has_special_permission.userid = @userid; ", d2.con);
                cmd1.Parameters.AddWithValue("@userid", userid);
                SqlDataReader permissions2 = cmd1.ExecuteReader();
                if (permissions2.HasRows)
                {
                    while (permissions2.Read())
                    {
                        var permission1 = new Permission();
                        permission1.permissionId = Convert.ToInt32(permissions2["permissionid"].ToString());
                        permission1.permissionName = permissions2["permsission"].ToString();
                        userpermission.Add(permission1);
                    }
                }
                else
                {

                }
                permissions2.Close();
                d2.con.Close();
                return userpermission;
                /*ViewBag.permission = userpermission;*/

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public IActionResult DashBoard()
        {
            ViewBag.permission = GetPermission();
            return View("DashBoard");
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
