using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class DeleteUserController : Controller
    {
		
		
		[HttpGet]
        public void Delete(int id)
        {
				try
				{
					DbConnect d5 = new DbConnect();
					d5.Connect();
					int useid =id;
					SqlCommand cmd = new SqlCommand("update tbl_user_credential set is_active=0 where userid=@userid;", d5.con);
					cmd.Parameters.AddWithValue("@userid", useid);
					int num = cmd.ExecuteNonQuery();
					if (num > 0)
					{
						Response.Redirect("/Permission/ExecutePermission/?permissionid=4");
					}
					else
					{
						Response.Redirect("/Permission/ExecutePermission/?permissionid=4");
					}

				}
				catch (Exception)
				{

					throw;
				}
		}
    }
}
