using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// <summary>
    /// Логика взаимодействия для TaskElementUserControl.xaml
    /// </summary>
    public partial class TaskElementUserControl : UserControl
    {
        public TaskElementUserControl()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan( 0, 0, 55);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
            

        }
        private void timerTick(object sender, EventArgs e)
        {
           
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimePicker_vis.Visibility == Visibility.Visible)
            {
                TimePicker_vis.Visibility = Visibility.Hidden;
            }
            else if (TimePicker_vis.Visibility == Visibility.Hidden)
            {
                TimePicker_vis.Visibility = Visibility.Visible;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
                Border_cursor.BorderBrush = Brushes.Gray;
        }

        private void CheckBox_heading_Unchecked(object sender, RoutedEventArgs e)
        {
            Border_cursor.BorderBrush = Brushes.LightGray;
        }
    }
}
