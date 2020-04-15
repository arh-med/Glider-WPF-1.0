using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glider_WPF_1._0
{
    class RegistrationViewModel : ViewModel 
    {
        Registration registration;
        private string user;
        private string password;
        private string company;
        private string adress;
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                Set(ref user, value);
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                Set(ref password, value);
            }
        }
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                Set(ref company, value);
            }
        }
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                Set(ref adress, value);
            }
        }
        private ICommand registrationUser;
        public ICommand RegistrationUser
        {
            get
            {
                return registrationUser ?? (registrationUser = new CommandExecutor(() =>
               {
                   User user = new User(User, Password, Company, Adress);
                   GliderDataContext gliderDataContext = GliderDataContext.Instance;
                   gliderDataContext.Users.Add(user);
                   gliderDataContext.SaveChanges();
                   User = "";
                   Password = "";
                   Company = "";
                   Adress = "";
                   registration.Close();
               }));
            }
        }

        public RegistrationViewModel(Registration registration)
        {
           this.registration = registration;
        }
    }
}
