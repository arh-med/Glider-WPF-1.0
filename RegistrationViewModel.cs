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
        public string User { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Adress { get; set; }
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

               }));
            }
        }
    }
}
