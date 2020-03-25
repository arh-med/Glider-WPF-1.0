using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glider_WPF_1._0
{
    public class UserMail
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string BodyMessage { get; set; }
        public DateTime TimeMessage { get; set; }
        public bool Done { get; set; }
    }
}
