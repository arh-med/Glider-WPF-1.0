using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Glider_WPF_1._0.UserControlMail
{
   
    public partial class MailUserControl : UserControl
    {
        
        MailUserControlViewModel mailUserControl;
        public string Login { get; set; }
        public System.Windows.Threading.DispatcherTimer timer;
        public MailUserControl(string Login)
        {
            InitializeComponent();
            this.Login = Login;
            mailUserControl = new MailUserControlViewModel(this);
            DataContext = mailUserControl;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += mailUserControl.eventTimer;
            timer.Start();
        }
        private void ComboBoxRecipients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mailUserControl.eventSelection.Invoke(this, null);
        }
        public void ComboBoxRecipients_SelectionChanged()
        {
            mailUserControl.eventSendMessage.Invoke(this, null);
        }
    }
}
