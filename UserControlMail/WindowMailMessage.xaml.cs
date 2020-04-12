using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace Glider_WPF_1._0.UserControlMail
{
    /// <summary>
    /// Логика взаимодействия для WindowMailMessage.xaml
    /// </summary>
    public partial class WindowMailMessage : Window
    {


        MailUserControl mailUserControl;
        public WindowMailMessage( MailUserControl mailUserControl, UserMail message)
        {
            InitializeComponent();
            this.mailUserControl = mailUserControl;
            WindowMailMessageViewModel windowMailMessageViewModel = new WindowMailMessageViewModel(mailUserControl, message,this);
            DataContext = windowMailMessageViewModel;
            this.mailUserControl.timer.Stop();
            this.mailUserControl.ComboBoxRecipients_SelectionChanged();
        }
    }
}
