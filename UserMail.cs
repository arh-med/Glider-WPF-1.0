﻿using System;

namespace Glider_WPF_1._0
{
    public class UserMail
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string BodyMessage { get; set; }
        public DateTime TimeMessage { get; set; }
        public bool Done { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public UserMail(string Heading, string BodyMessage, string Sender, string Recipient)
        {
            this.Heading = Heading;
            this.BodyMessage = BodyMessage;
            this.Sender = Sender;
            this.Recipient = Recipient;
            TimeMessage = DateTime.Now;
        }
        public UserMail()
        {

        }
    }
}
