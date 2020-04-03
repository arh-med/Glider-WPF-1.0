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
        public ObservableCollection<UserMail> Mails { get; set; }
       
        string Login { get; set; }
        public List<User> Users { get; set; }
        public System.Windows.Threading.DispatcherTimer timer;
        public MailUserControl(string Login)
        {
            InitializeComponent();
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            Users = new List<User>(GliderDataContext.Instance.Users.ToList());
            this.Login = Login;
            Mails = new ObservableCollection<UserMail>();
            User this_user = gliderDataContext.Users.FirstOrDefault(u => u.Login == Login);
            Users.Remove(this_user);
            DataContext = this;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += (sender, e) =>
            {

                ObservableCollection<UserMail> MailsTimer = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                foreach (UserMail item in MailsTimer)
                {
                    if (item.Recipient == Login && item.Done == false )
                    {
                        WindowMailMessage windowMailMessage = new WindowMailMessage(this,item);
                        windowMailMessage.DataContext = item;
                        windowMailMessage.Show();
                    }
                }

            };
            timer.Start();
        }

       

        private void AddMessageButton(object sender, RoutedEventArgs e)
        {
            UserMail message = new UserMail();
            message.Heading = Heading_txt.Text;
            message.BodyMessage = Message_txt.Text;
            message.TimeMessage = DateTime.Now;
            message.Sender = Login;
            message.Recipient = ComboBoxRecipients.Text;
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            gliderDataContext.UserMail.Add(message);
            gliderDataContext.SaveChanges();
            Mails.Add(message);

            MessageUserControl messag = new MessageUserControl();
            messag.DataContext = message;
            messag.HorizontalAlignment = HorizontalAlignment.Right;
            StackPanelMessage.Children.Add(messag);

            Heading_txt.Clear();
            Message_txt.Clear();


        }



        private void ComboBoxRecipients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxRecipients.SelectedItem !=null)
            {
                StackPanelMessage.Children.Clear();
                ObservableCollection<UserMail> MailsSort = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                Mails.Clear();
                User recCombobox = (User)ComboBoxRecipients.SelectedItem;
                foreach (UserMail item in MailsSort)
                {
                    if (item.Sender == Login && item.Recipient == recCombobox.Login || item.Sender == recCombobox.Login && item.Recipient == Login)
                        Mails.Add(item);
                }
                foreach (UserMail item in Mails)
                {
                    if (item.Sender == Login)
                    {
                        MessageUserControl message = new MessageUserControl();
                        message.DataContext = item;
                        message.HorizontalAlignment = HorizontalAlignment.Right;
                        StackPanelMessage.Children.Add(message);

                    }
                    else if (item.Sender == recCombobox.Login)
                    {
                        MessageRecipientsUserControl message = new MessageRecipientsUserControl();
                        message.DataContext = item;
                        message.HorizontalAlignment = HorizontalAlignment.Left;
                        StackPanelMessage.Children.Add(message);

                    }
                }
            }
            else
            {
                StackPanelMessage.Children.Clear();
            }
            ScrollViewrMessage.ScrollToBottom();

        }
        public void ComboBoxRecipients_SelectionChanged()
        {
            if (ComboBoxRecipients.Text != null)
            {
                StackPanelMessage.Children.Clear();
                ObservableCollection<UserMail> MailsSort = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                Mails.Clear();
                User recCombobox = (User)ComboBoxRecipients.SelectedItem;
                foreach (UserMail item in MailsSort)
                {
                    if (item.Sender == Login && item.Recipient == recCombobox.Login || item.Sender == recCombobox.Login && item.Recipient == Login)
                        Mails.Add(item);
                }
                foreach (UserMail item in Mails)
                {
                    if (item.Sender == Login)
                    {
                        MessageUserControl message = new MessageUserControl();
                        message.DataContext = item;
                        message.HorizontalAlignment = HorizontalAlignment.Right;
                        StackPanelMessage.Children.Add(message);

                    }
                    else if (item.Sender == recCombobox.Login)
                    {
                        MessageRecipientsUserControl message = new MessageRecipientsUserControl();
                        message.DataContext = item;
                        message.HorizontalAlignment = HorizontalAlignment.Left;
                        StackPanelMessage.Children.Add(message);

                    }
                }
            }
            else
            {
                StackPanelMessage.Children.Clear();
            }
            ScrollViewrMessage.ScrollToBottom();
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            User recCombobox = (User)ComboBoxRecipients.SelectedItem;
            StackPanelMessage.Children.Clear();
            string filter = txt_Search.Text;
            for (int i = 0; i < Mails.Count; i++)
            {
                if (true == Mails[i].BodyMessage.ToUpper().StartsWith(filter.ToUpper()) && txt_Search.Text != "")
                {
                        if (Mails[i].Sender == Login)
                        {
                            MessageUserControl message = new MessageUserControl();
                            message.DataContext = Mails[i];
                            message.HorizontalAlignment = HorizontalAlignment.Right;
                            StackPanelMessage.Children.Add(message);

                        }
                        else if (Mails[i].Sender == recCombobox.Login)
                        {
                            MessageRecipientsUserControl message = new MessageRecipientsUserControl();
                            message.DataContext = Mails[i];
                            message.HorizontalAlignment = HorizontalAlignment.Left;
                            StackPanelMessage.Children.Add(message);
                        }
                    
                }
               
            }

        }
    }
}
