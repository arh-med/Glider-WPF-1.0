namespace Glider_WPF_1._0
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public bool Run { get; set; }

        public User(string login, string password,string company,string adress)
        {
            Login = login;
            Password = password;
            Company = company;
            Address = adress;
        }
        public User()
        {

        }

    }
}
