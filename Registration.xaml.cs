using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Glider_WPF_1._0
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Reg(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Login = txt_User.Text;
            user.Password = txt_Password.Password;
            user.Company = txt_Company.Text;
            user.Address = txt_Adress.Text;
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            gliderDataContext.Users.Add(user);
            gliderDataContext.SaveChanges();
        }

        private void ToggleButton_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
