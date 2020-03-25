using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Properties;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glider_WPF_1._0
{
    class MainWindowViewModel : ViewModel
    {
        private static string Encode(string Source, int Key = 3)
        {
            return new string(Source.Select(c => (char)(c + Key)).ToArray());
        }
        private static string Decode(string Source, int Key = 3)
        {
            return new string(Source.Select(c => (char)(c - Key)).ToArray());
        }

        protected bool checkRem = (bool)Settings.Default["Remember"];
        protected string loginText = Decode((string)Settings.Default["Login"]);
        protected string passworText = Decode((string)Settings.Default["Password"]);
        public bool CheckRem
        {
            get
            {
                return checkRem;
            }
            set
            {
                Set(ref checkRem, value);
            }
        }
        public string LoginText
        {
            get
            {
                return loginText;
            }
            set
            {
                Set(ref loginText, value);
            }
        }
        public string PassworText
        {
            get
            {
                return passworText;
            }
            set
            {
                Set(ref passworText, value);
            }
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new CommandExecutor(() =>
                {
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    string login = LoginText;
                    User user = gliderDataContext.Users.FirstOrDefault(u => u.Login == login);

                    if(user == null)
                    {
                        OnLoginFailed.Invoke(this, null);
                        return;
                    }

                    if(PassworText == user.Password)
                    {
                        OnLoginSucceded.Invoke(this, null);
                    }
                    else
                    {
                        OnLoginFailed.Invoke(this, null);
                        return;
                    }

                    if (CheckRem == true)
                    {
                        Settings.Default["Login"] = Encode(LoginText);
                        Settings.Default["Password"] = Encode(PassworText);
                        Settings.Default["Remember"] = true;
                        Settings.Default.Save();
                    }
                    else if (CheckRem == false)
                    {
                        Settings.Default["Login"] = Encode("");
                        Settings.Default["Password"] = Encode("");
                        Settings.Default["Remember"] = false;
                        Settings.Default.Save();
                    }
                }));
            }
        }

        public EventHandler OnLoginFailed { get; set; }
        public EventHandler OnLoginSucceded { get; set; }
    }
}
