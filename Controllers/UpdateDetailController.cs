using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using RegistrationMVC.Models;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class UpdateDetailController : Controller
    {
        public void UpdateDetail(UserDetail detail)
        {
            try
            {

                DbConnect d1 = new DbConnect();
                d1.Connect();

                SqlCommand cmd = new SqlCommand("update tbl_user_detail set firstname=@fname,lastname=@lname,mobileno=@mobileno,email = @email, city = @city, state = @state where userid = @userid;", d1.con);
                cmd.Parameters.AddWithValue("@userid", HomeController.userid);
                cmd.Parameters.AddWithValue("@fname", detail.Fname);
                cmd.Parameters.AddWithValue("@lname", detail.Lname);
                cmd.Parameters.AddWithValue("@email", detail.Email);
                cmd.Parameters.AddWithValue("@mobileno", detail.Phone);
                cmd.Parameters.AddWithValue("@city", detail.city);
                cmd.Parameters.AddWithValue("@state", detail.state);
                int num = cmd.ExecuteNonQuery();
                if (num > 0)
                {
                    Response.Redirect("/Permission/ExecutePermission/?permissionid=5");
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
