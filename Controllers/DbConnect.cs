using System.Data.SqlClient;

namespace RegistrationMVC.Controllers
{
    public class DbConnect
    {
        public SqlConnection con = null;
        public void Connect()
        {
            con = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = User_registration2; Trusted_Connection = True;");
            con.Open();
        }
    }
}
