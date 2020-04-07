using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glider_WPF_1._0
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime DateReport { get; set; }
        public string Revenue { get; set; }
        public string Order { get; set; }
        public string Checks { get; set; }
        public string ChecksAmount { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
        public string Company { get; set; }

    }
}
