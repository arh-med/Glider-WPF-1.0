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
        UserMail message;
        GliderDataContext gliderDataContext = GliderDataContext.Instance;
        public ObservableCollection<UserMail> Mails { get; set; }
        public WindowMailMessage( MailUserControl mailUserControl, UserMail message)
        {
            InitializeComponent();
            this.mailUserControl = mailUserControl;
            this.message = message;
            this.mailUserControl.timer.Stop();
            this.mailUserControl.ComboBoxRecipients_SelectionChanged();


        }

        private void ButtonClickDone(object sender, RoutedEventArgs e)
        {
            message.Done = true;
            gliderDataContext.Entry(message).State = EntityState.Modified;
            gliderDataContext.SaveChanges();
            mailUserControl.timer.Start();
            this.Close();
           
        }

        private void ButtonClickSend(object sender, RoutedEventArgs e)
        {

            UserMail messageSend = new UserMail();
            messageSend.Heading = Heading_txt.Text;
            messageSend.BodyMessage = Message_txt.Text;
            messageSend.TimeMessage = DateTime.Now;
            messageSend.Sender = this.message.Recipient;
            messageSend.Recipient = this.message.Sender;
            gliderDataContext.UserMail.Add(messageSend);

            message.Done = true;
            gliderDataContext.Entry(message).State = EntityState.Modified;
            gliderDataContext.SaveChanges();


            mailUserControl.timer.Start();
            this.Close();
        }
    }
}
