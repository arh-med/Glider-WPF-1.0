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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Glider_WPF_1._0.UserControlTask
{
   
    public partial class TaskUserControl : UserControl 
    {
        TaskUserControlViewModel taskUserControlViewModel;
        public System.Windows.Threading.DispatcherTimer timer;
        public string Login { get; set; }
        public TaskUserControl(string Login)
        {
            InitializeComponent();
            this.Login = Login;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            taskUserControlViewModel = new TaskUserControlViewModel(this);
            DataContext = taskUserControlViewModel;
            timer.Tick += taskUserControlViewModel.eventTimer;
            timer.Start();
        }
    }
}
