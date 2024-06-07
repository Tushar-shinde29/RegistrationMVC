namespace RegistrationMVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Password { get; set; }
        public int Role { get; set; }
        public string ?Salt { get; set; }

        public bool is_active { get; set; }

    }
}
