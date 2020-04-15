namespace Glider_WPF_1._0
{
    public class Request
    {
        public int Id { get; private set; }
        public string Nomination { get; set; }
        public string Quantity { get; set; }
        public string CustomerPhone { get; set; }
        public string Company { get; set; }

        public Request (string Nomination,string Quantity, string CustomerPhone, string Company)
        {
            this.Nomination = Nomination;
            this.Quantity = Quantity;
            this.CustomerPhone = CustomerPhone;
            this.Company = Company;
        }
        public Request()
        {

        }
    }
}
