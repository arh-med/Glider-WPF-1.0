using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Glider_WPF_1._0.UserControlMail
{
    class MailUserControlViewModel : ViewModel
    {
        private string heading;
        private string message;
        public string Heading
        {
            get
            {
                return heading;
            }
            set
            {
                Set(ref heading, value);
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                Set(ref message, value);
            }
        }
        MailUserControl mailUserControl;
        public EventHandler eventSelection;
        public EventHandler eventSendMessage;
        public EventHandler eventTimer;
        public ObservableCollection<UserMail> mails = new ObservableCollection<UserMail>();
        public ObservableCollection<UserMail> Mails
        {
            get
            {
                return mails;
            }
            
        }
        private ICommand addMessage;
        public ICommand AddMessage
        {
            get
            {
                return addMessage ?? (addMessage = new CommandExecutor(() =>
                {
                    UserMail message = new UserMail(Heading, Message, mailUserControl.Login, mailUserControl.ComboBoxRecipients.Text);
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    gliderDataContext.UserMail.Add(message);
                    gliderDataContext.SaveChanges();
                    mails.Add(message);

                    MessageUserControl messag = new MessageUserControl();
                    messag.DataContext = message;
                    messag.HorizontalAlignment = HorizontalAlignment.Right;
                    mailUserControl.StackPanelMessage.Children.Add(messag);

                    Heading = "";
                    Message = "";
                }));

            }
        }
        private List<User> users = new List<User>();
        public List<User> Users
        {
            get
            {
                return users;
            }
        }
        UserMail userSender = new UserMail();

        public MailUserControlViewModel(MailUserControl mailUserControl)
        {
            this.mailUserControl = mailUserControl;
            eventSelection = (sender, e) =>
            {
                if (mailUserControl.ComboBoxRecipients.SelectedItem != null)
                {
                    mailUserControl.StackPanelMessage.Children.Clear();
                    ObservableCollection<UserMail> MailsSort = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                    mails.Clear();
                    User recCombobox = (User)mailUserControl.ComboBoxRecipients.SelectedItem;
                    foreach (UserMail item in MailsSort)
                    {
                        if (item.Sender == mailUserControl.Login && item.Recipient == recCombobox.Login || item.Sender == recCombobox.Login && item.Recipient == mailUserControl.Login)
                            mails.Add(item);
                    }
                    foreach (UserMail item in Mails)
                    {
                        if (item.Sender == mailUserControl.Login)
                        {
                            MessageUserControl message = new MessageUserControl();
                            message.DataContext = item;
                            message.HorizontalAlignment = HorizontalAlignment.Right;
                            mailUserControl.StackPanelMessage.Children.Add(message);

                        }
                        else if (item.Sender == recCombobox.Login)
                        {
                            MessageRecipientsUserControl message = new MessageRecipientsUserControl();
                            message.DataContext = item;
                            message.HorizontalAlignment = HorizontalAlignment.Left;
                            mailUserControl.StackPanelMessage.Children.Add(message);

                        }
                    }
                }
                else
                {
                    mailUserControl.StackPanelMessage.Children.Clear();
                }
                mailUserControl.ScrollViewrMessage.ScrollToBottom();

            };
            users = new List<User>(GliderDataContext.Instance.Users.ToList());
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            User this_user = gliderDataContext.Users.FirstOrDefault(u => u.Login == mailUserControl.Login);
            users.Remove(this_user);
            eventSendMessage = (sender, e) =>
            {
                if (mailUserControl.ComboBoxRecipients.Text != null)
                {
                    mailUserControl.ComboBoxRecipients.Text = userSender.Sender;
                    mailUserControl.StackPanelMessage.Children.Clear();
                    ObservableCollection<UserMail> MailsSort = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                    mails.Clear();
                    foreach (UserMail item in MailsSort)
                    {
                        if (item.Sender == mailUserControl.Login && item.Recipient == mailUserControl.ComboBoxRecipients.Text || item.Sender == mailUserControl.ComboBoxRecipients.Text && item.Recipient == mailUserControl.Login)
                            mails.Add(item);
                    }
                    foreach (UserMail item in Mails)
                    {
                        if (item.Sender == mailUserControl.Login)
                        {
                            MessageUserControl message = new MessageUserControl();
                            message.DataContext = item;
                            message.HorizontalAlignment = HorizontalAlignment.Right;
                            mailUserControl.StackPanelMessage.Children.Add(message);

                        }
                        else if (item.Sender == mailUserControl.ComboBoxRecipients.Text)
                        {
                            MessageRecipientsUserControl message = new MessageRecipientsUserControl();
                            message.DataContext = item;
                            message.HorizontalAlignment = HorizontalAlignment.Left;
                            mailUserControl.StackPanelMessage.Children.Add(message);

                        }
                    }
                }
                else
                {
                    mailUserControl.StackPanelMessage.Children.Clear();
                }
                mailUserControl.ScrollViewrMessage.ScrollToBottom();
            };
            eventTimer = (sender, e) =>
            {
                ObservableCollection<UserMail> MailsTimer = new ObservableCollection<UserMail>(GliderDataContext.Instance.UserMail.ToList());
                foreach (UserMail item in MailsTimer)
                {
                    if (item.Recipient == mailUserControl.Login && item.Done == false)
                    {
                        userSender.Sender = item.Sender;
                        WindowMailMessage windowMailMessage = new WindowMailMessage(mailUserControl, item);
                        windowMailMessage.Show();
                    }
                }
            };
        }

    }
}
