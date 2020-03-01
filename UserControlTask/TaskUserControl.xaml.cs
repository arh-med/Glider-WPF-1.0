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
    /// <summary>
    /// Логика взаимодействия для TaskUserControl.xaml
    /// </summary>
    public partial class TaskUserControl : UserControl
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public TaskUserControl()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.Heading = Heading_txt.Text;
            task.Tast = Task_txt.Text;

            DateTime? date = Data_txt.SelectedDate;
            DateTime? time = PresetTimePicker.SelectedTime;

            DateTime dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
            task.Alarm = dateTime;
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            gliderDataContext.Tasks.Add(task);
            gliderDataContext.SaveChanges();

            Tasks.Add(task);

        }
    }
}
