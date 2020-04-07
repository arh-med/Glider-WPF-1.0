using Glider_WPF_1._0.Sql;
using Glider_WPF_1._0.UserControlMail;
using Glider_WPF_1._0.UserControlReport;
using Glider_WPF_1._0.UserControlRequest;
using Glider_WPF_1._0.UserControlSaver;
using Glider_WPF_1._0.UserControlTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
   
    public partial class Home : Window
    {
        
        RequestUserControl request;
        TaskUserControl task;
        MailUserControl mailUser;
        ReportUserControl report;
        SupportUserControl supportUser; 
        public Home(string Login)
        {
            InitializeComponent();
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            User user = gliderDataContext.Users.FirstOrDefault(u => u.Login == Login);
            string company = user.Company;
            request = new RequestUserControl(company);
            task = new TaskUserControl(Login);
            mailUser = new MailUserControl(Login);
            report = new ReportUserControl(company);
            supportUser = new SupportUserControl();
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
            if (GridMenu.Width == 200)
                 this.ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));   
        }
        private void TaskMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(task);
            if (GridMenu.Width == 200)
                this.ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void MailMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(mailUser);
            if (GridMenu.Width == 200)
                this.ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void ReportMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(report);
            if (GridMenu.Width == 200)
                this.ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void SupportMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridAddUserControl.Children.Clear();
            GridAddUserControl.Children.Add(supportUser);
            if (GridMenu.Width == 200)
                this.ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
