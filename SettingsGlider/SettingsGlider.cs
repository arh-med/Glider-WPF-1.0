using Glider_WPF_1._0.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq;

namespace Glider_WPF_1._0.SettingsGlider
{
    public static class SettingsGlider
    {
        private static string Encode (string Source, int Key = 3)
        {
            return new string(Source.Select(c => (char)(c + Key)).ToArray());
        }
        private static string Decode(string Source, int Key = 3)
        {
            return new string(Source.Select(c => (char)(c - Key)).ToArray());
        }



        public static void CheckStart(CheckBox checkBox, TextBox textBoxLogin, PasswordBox textBoxPassword)
        {
            checkBox.IsChecked = (bool)Settings.Default["Remember"];
            if (checkBox.IsChecked == true)
            {
                
                textBoxLogin.Text = Decode((string)Settings.Default["Login"]);
                textBoxPassword.Password = Decode((string)Settings.Default["Password"]);
            }
            else
            {
                textBoxLogin.Text = "";
                textBoxPassword.Password = "";
            }
        }
       public  static void CheckRemember (CheckBox checkBox, TextBox textBoxLogin, PasswordBox textBoxPassword)
        {
            if (checkBox.IsChecked == true)
            {
                Settings.Default["Login"] = Encode(textBoxLogin.Text);
                Settings.Default["Password"] = Encode(textBoxPassword.Password);
                Settings.Default["Remember"] = true;
                Settings.Default.Save();
            }
            else if (checkBox.IsChecked == false)
            {
                Settings.Default["Login"] = "";
                Settings.Default["Password"] = "";
                Settings.Default["Remember"] = false;
                Settings.Default.Save();
            }
        }
    }
}
