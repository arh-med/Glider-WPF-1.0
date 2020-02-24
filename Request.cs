
using System;

namespace Glider_WPF_1._0
{
    public class Request
    {
        public Guid Id { get; private set; }
        public string Nomination { get; set; }
        public string Quantity { get; set; }
        public string CustomerPhone { get; set; }

        public Request()
        {
            Id = Guid.NewGuid();
        }
    }
}
