using System;

namespace Glider_WPF_1._0
{
    public class Task
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Tast { get; set; }
        public DateTime Alarm { get; set; }
        public DateTime Replay { get; set; }
        public bool Done { get; set; }
        public string Login { get; set; }
        public Task(string heading,string task,DateTime alarm, string login)
        {
            Heading = heading;
            Tast = task;
            Alarm = alarm;
            Login = login;
        }
        public Task()
        {

        }
    }
}
