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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Glider_WPF_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          
            GliderDataContext gliderDataContext =  GliderDataContext.Instance;
            string login = txt_Login.Text;
            User user = gliderDataContext.Users.FirstOrDefault(u => u.Login == login);

            if(user == null)
            {
                lable_txt.Content = "Incorrect username or password";
            }
            else
            {
                if (txt_Password.Password == user.Password)
                {
                    Home home = new Home();
                    home.Show();
                }
                else
                {
                    lable_txt.Content = "Incorrect username or password";
                }
            }
        }

        private void ToggleButton_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
