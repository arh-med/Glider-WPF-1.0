using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glider_WPF_1._0.UserControlMail
{
    class WindowMailMessageViewModel : ViewModel
    {
        UserMail message;
        MailUserControl mailUserControl;
        WindowMailMessage windowMailMessage;
        private string bodyMessage;
        public string BodyMessage
        {
            get
            {
                return bodyMessage;
            }
            set
            {
                Set(ref bodyMessage, value);
            }
        }
        private string heading;
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
        private DateTime timeMessage;
        public DateTime TimeMessage
        {
            get
            {
                return timeMessage;
            }
            set
            {
                Set(ref timeMessage, value);
            }
        }
        private string headingSend;
        public string HeadingSend
        {
            get
            {
                return headingSend;
            }
            set
            {
                Set(ref headingSend, value);
            }
        }
        private string bodyMessageSend;
        public string BodyMessageSend
        {
            get
            {
                return bodyMessageSend;
            }
            set
            {
                Set(ref bodyMessageSend, value);
            }
        }
        private ICommand doneMessage;
        public ICommand DoneMessage
        {
            get
            {
                return doneMessage ?? (doneMessage = new CommandExecutor(() => 
                {
                    message.Done = true;
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    gliderDataContext.Entry(message).State = EntityState.Modified;
                    gliderDataContext.SaveChanges();
                    mailUserControl.timer.Start();

                    windowMailMessage.Close();
                }));
            }
        }
        private ICommand sendMessage;
        public ICommand SendMessage
        {
            get
            {
                return sendMessage ?? (sendMessage = new CommandExecutor(() => 
                {
                    UserMail messageSend = new UserMail(HeadingSend, BodyMessageSend, message.Recipient, message.Sender);
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    gliderDataContext.UserMail.Add(messageSend);
                    message.Done = true;
                    gliderDataContext.Entry(message).State = EntityState.Modified;
                    gliderDataContext.SaveChanges();


                    mailUserControl.timer.Start();
                    windowMailMessage.Close();
                }));
            }
        }
        public WindowMailMessageViewModel(MailUserControl mailUserControl, UserMail message, WindowMailMessage windowMailMessage)
        {
            this.windowMailMessage = windowMailMessage;
            this.mailUserControl = mailUserControl;
            this.message = message;
            BodyMessage = message.BodyMessage;
            Heading = message.Heading;
            TimeMessage = message.TimeMessage;
        }
    }
}
