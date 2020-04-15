using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace Glider_WPF_1._0.UserControlRequest
{
    
    public partial class RequestUserControl : UserControl
    {
        RequestUserControlViewModel requestUserControlViewModel;
        string Company { get; set; }
        public RequestUserControl(string company)
        {
            InitializeComponent();
            Company = company;
            requestUserControlViewModel = new RequestUserControlViewModel(Company,this);   
            DataContext = requestUserControlViewModel;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += requestUserControlViewModel.eventTimer;
            timer.Start();
        }
    }
}
