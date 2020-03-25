using Glider_WPF_1._0.UserControlMail;
using Glider_WPF_1._0.UserControlReport;
using Glider_WPF_1._0.UserControlRequest;
using Glider_WPF_1._0.UserControlTask;
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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        RequestUserControl request = new RequestUserControl();
        TaskUserControl task = new TaskUserControl();
        MailUserControl mailUser = new MailUserControl();
        ReportUserControl report = new ReportUserControl();
        public Home()
        {
            InitializeComponent();
            
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void RequestMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(request);
        }

   
        private void TaskMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(task);
        }

        private void MailMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(mailUser);
        }

        private void ReportMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(report);
        }
    }
}
