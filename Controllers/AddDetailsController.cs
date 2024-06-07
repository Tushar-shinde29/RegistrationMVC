using Microsoft.AspNetCore.Mvc;
using RegistrationMVC.Models;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class AddDetailsController : Controller
    {
        [HttpPost]
        public void Adddetail(UserDetail detail)
        {
            try
            {

                DbConnect d1 = new DbConnect();
                d1.Connect();

                SqlCommand cmd = new SqlCommand("insert into tbl_user_detail(userid,firstname,lastname,email,mobileno,city,state) values " +
                    "(@userid,@fname,@lname,@email,@mobileno,@city,@state); ", d1.con);
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
                    Response.Redirect("/Home/DashBoard/?role=" + HomeController.roleid + "&userid=" + HomeController.userid);
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
