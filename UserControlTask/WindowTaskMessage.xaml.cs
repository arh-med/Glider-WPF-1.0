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
        public List<Task> Tasks { get; set; }
        TaskUserControl taskUserControl;
        public WindowTaskMessage(Task task, TaskUserControl taskUserControl)
        {
            InitializeComponent();

            Tasks = new List<Task>();
            Tasks.Add(task);
            ItemsControlTask.ItemsSource = Tasks;
            this.taskUserControl = taskUserControl;
            Heading_txt.Text = task.Heading;
            Task_txt.Text = task.Tast;
            Data_txt.SelectedDate = task.Alarm;
            PresetTimePicker.SelectedTime = task.Alarm;
        }

        private void DoneButtonClik(object sender, RoutedEventArgs e)
        {
            Task taskWindow = null;
            foreach (Task item in Tasks)
            {
                taskWindow = item;
            }
            taskUserControl.RemoveButtonClick(taskWindow);
            this.Close();
        }

        private void RenameButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Task taskWindow = null;
                foreach (Task item in Tasks)
                {
                    taskWindow = item;
                }
                taskUserControl.RemoveButtonClick(taskWindow);

                Task task = new Task();
                task.Heading = Heading_txt.Text;
                task.Tast = Task_txt.Text;

                DateTime? date = Data_txt.SelectedDate;
                DateTime? time = PresetTimePicker.SelectedTime;

                DateTime dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
                task.Alarm = dateTime;
                taskUserControl.AddButtonClick(task);
                this.Close();
            }
            catch 
            {

                MessageBox.Show("Fill in all lines");
            }
            


        }
    }
}
