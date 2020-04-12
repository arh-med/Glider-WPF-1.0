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
using System.Windows.Shapes;

namespace Glider_WPF_1._0.UserControlTask
{
    
    public partial class WindowTaskMessage : Window
    {
        TaskUserControl taskUserControl;
        WindowTaskMessageViewModel windowTaskMessageViewModel;
        public WindowTaskMessage(Task task, TaskUserControl taskUserControl)
        {
            InitializeComponent();
            this.taskUserControl = taskUserControl;
            windowTaskMessageViewModel = new WindowTaskMessageViewModel(task, taskUserControl, this);
            DataContext = windowTaskMessageViewModel;
            ItemsControlTask.ItemsSource = windowTaskMessageViewModel.Tasks;
        }
    }
}
