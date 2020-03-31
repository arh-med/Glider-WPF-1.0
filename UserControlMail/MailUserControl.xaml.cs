﻿using Glider_WPF_1._0.Sql;
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
        GliderDataContext gliderDataContext = GliderDataContext.Instance;
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
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += (sender, e) =>
            {
               

            };
            timer.Start();
        }

       

        private void AddMessageButton(object sender, RoutedEventArgs e)
        {
            User recCombobox = (User)ComboBoxRecipients.SelectedItem;
            UserMail message = new UserMail();
            message.Heading = Heading_txt.Text;
            message.BodyMessage = Message_txt.Text;
            message.TimeMessage = DateTime.Now;
            message.Sender = Login;
            message.Recipient = ComboBoxRecipients.Text;           
            gliderDataContext.UserMail.Add(message);
            gliderDataContext.SaveChanges();
            Mails.Add(message);
            if (message.Sender == Login)
            {
                MessageUserControl messag = new MessageUserControl();
                messag.DataContext = message;
                messag.HorizontalAlignment = HorizontalAlignment.Right;
                StackPanelMessage.Children.Add(messag);
            }
            else if (message.Sender == recCombobox.Login)
            {
                MessageRecipientsUserControl messag = new MessageRecipientsUserControl();
                messag.DataContext = message;
                messag.HorizontalAlignment = HorizontalAlignment.Left;
                StackPanelMessage.Children.Add(messag);
            }
        }

       

        private void ComboBoxRecipients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
    }
}
