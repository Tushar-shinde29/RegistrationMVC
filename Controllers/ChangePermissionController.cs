using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class ChangePermissionController : Controller
    {
        [HttpGet("ChangePermission")]
        public IActionResult Index()
        {
            DbConnect d2 = new DbConnect();
            List<int> ids = new List<int>();
            int role = Convert.ToInt32(HttpContext.Request.Query["roleid"]);
            int userid = Convert.ToInt32(HttpContext.Request.Query["userid"]);
            Console.WriteLine(role +  userid);
            try
            {
                d2.Connect();
                SqlCommand cmd = new SqlCommand("select tbl_roles_has_permissions.permission as permissionid,tbl_permission.permsission from tbl_roles_has_permissions left join tbl_permission on tbl_roles_has_permissions.permission=tbl_permission.permission_id where tbl_roles_has_permissions.role_id=@role", d2.con);
                cmd.Parameters.AddWithValue("@role", role);
                SqlDataReader permissions = cmd.ExecuteReader();
                List<Permission> rolehaspermission = new List<Permission>();
                if (permissions.HasRows)
                {
                    while (permissions.Read())
                    {
                        var permission1 = new Permission();
                        permission1.permissionId = Convert.ToInt32(permissions["permissionid"].ToString());
                        permission1.permissionName = permissions["permsission"].ToString();
                        rolehaspermission.Add(permission1);
                    }
                }
                
                permissions.Close();
                SqlCommand cmd1 = new SqlCommand("select tbl_user_has_special_permission.permission as permissionid," +
                    "tbl_permission.permsission from tbl_user_has_special_permission left join tbl_permission on " +
                    "tbl_user_has_special_permission.permission = tbl_permission.permission_id " +
                    "where tbl_user_has_special_permission.userid = @userid; ", d2.con);
                cmd1.Parameters.AddWithValue("@userid", userid);
                SqlDataReader permissions2 = cmd1.ExecuteReader();
                List<Permission> userhaspermission = new List<Permission>();
                
                if (permissions2.HasRows)
                {
                    
                    while (permissions2.Read())
                    {
                        var permission1 = new Permission();
                        permission1.permissionId = Convert.ToInt32(permissions2["permissionid"].ToString());
                        permission1.permissionName = permissions2["permsission"].ToString();
                        userhaspermission.Add(permission1);
                         
                    }
                    
                }
                else
                {

                }
                permissions2.Close();
                SqlCommand cmd3 = new SqlCommand("select * from tbl_permission", d2.con);
                SqlDataReader permissions3 = cmd3.ExecuteReader();
                List<Permission> totalpermission = new List<Permission>();
                if (permissions3.HasRows)
                {
                    while (permissions3.Read())
                    {
                        var permission3 = new Permission();
                        permission3.permissionId = Convert.ToInt32(permissions3["permission_id"].ToString());
                        permission3.permissionName = permissions3["permsission"].ToString();
                        totalpermission.Add(permission3);
                    }
                }
                
                d2.con.Close();
                
                for (int i = 0; i < rolehaspermission.Count; i++)
                {

                    ids.Add(rolehaspermission[i].permissionId);
                }
                for (int i = 0; i < userhaspermission.Count; i++)
                {

                    ids.Add(userhaspermission[i].permissionId);
                }
                ViewBag.prid = ids;
                ViewBag.rolepermission = rolehaspermission;
                ViewBag.userpermission = userhaspermission;
                ViewBag.totalpermission = totalpermission;
            }
            catch (Exception)
            {

                throw;
            }
            return View("changePermission");
        }
        public JsonResult changepr()
        {
            DbConnect d2 = new DbConnect();
            try
            {
                d2.Connect();
                SqlCommand cmd = new SqlCommand("exec give_user_permission @userid,1,@prid", d2.con);
                cmd.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Request.Query["userid"]));
                cmd.Parameters.AddWithValue("@prid", Convert.ToInt32(HttpContext.Request.Query["prid"]));
                int num = cmd.ExecuteNonQuery();
                return Json(num);
            }
            catch { 
                
                throw;
            }
        }
        public JsonResult removepr()
        {
            DbConnect d2 = new DbConnect();
            try
            {
                d2.Connect();
                SqlCommand cmd = new SqlCommand("exec give_user_permission @userid,2,@prid", d2.con);
                cmd.Parameters.AddWithValue("@userid", Convert.ToInt32(HttpContext.Request.Query["userid"]));
                cmd.Parameters.AddWithValue("@prid", Convert.ToInt32(HttpContext.Request.Query["prid"]));
                int num = cmd.ExecuteNonQuery();
                return Json(num);
            }
            catch
            {

                throw;
            }
        }
    }
}
