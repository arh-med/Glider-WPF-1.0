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
        string Login { get; set; }
        public TaskUserControl(string Login)
        {
            InitializeComponent();
            Tasks = new ObservableCollection<Task>();
            this.Login = Login;
            ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
            foreach (Task tas in TaskSort)
            {
                if (tas.Login == Login)
                    Tasks.Add(tas);
            }
            DataContext = this;

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += (sender, e) => 
            {
                foreach (var task in Tasks)
                {
                    DateTime dateTimeAlarm = new DateTime(task.Alarm.Year,task.Alarm.Month,task.Alarm.Day,task.Alarm.Hour,task.Alarm.Minute,0);
                    DateTime dateTimeNow = DateTime.Now;
                    DateTime dateTimeEqual = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, dateTimeNow.Minute, 0);
                    if (dateTimeAlarm <= dateTimeEqual)
                    {
                        
                        WindowTaskMessage windowTaskMessage = new WindowTaskMessage(task,this);
                        windowTaskMessage.Show();
                    }
                }
            };
            timer.Start();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Task task = new Task();
                task.Heading = Heading_txt.Text;
                task.Tast = Task_txt.Text;

                DateTime? date = Data_txt.SelectedDate;
                DateTime? time = PresetTimePicker.SelectedTime;

                DateTime dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
                task.Alarm = dateTime;
                task.Login = Login;
                GliderDataContext gliderDataContext = GliderDataContext.Instance;
                gliderDataContext.Tasks.Add(task);
                gliderDataContext.SaveChanges();

                Tasks.Add(task);
            }
            catch 
            {

                MessageBox.Show("Fill in all lines");
            }
            Heading_txt.Text = "";
            Task_txt.Text = "";
            Data_txt.SelectedDate = null;
            PresetTimePicker.SelectedTime = null;
        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {

            try
            {
                foreach (Task taskElementUser in ItemsControlTask.Items.SourceCollection)
                {
                    if (taskElementUser.Done == true)
                    {
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Tasks.Remove(taskElementUser);
                        gliderDataContext.SaveChanges();
                    }
                }
                ItemsControlTask.ItemsSource = null;
                Tasks.Clear();
                ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                foreach (Task tas in TaskSort)
                {
                    if (tas.Login == Login)
                        Tasks.Add(tas);
                }
                ItemsControlTask.ItemsSource = Tasks;
            }
            catch (Exception ex)
            {

                MessageBox.Show(Convert.ToString(ex));
            }
        }

        private void InduceButtonClick(object sender, RoutedEventArgs e)
        {
            Task taskInduce=null;
            foreach (Task taskElementUser in ItemsControlTask.Items.SourceCollection)
            {
                if (taskElementUser.Done == true)
                {
                    taskInduce = taskElementUser;
                }
            }
            if (taskInduce!=null)
            {
                WindowTaskMessage windowTaskMessage = new WindowTaskMessage(taskInduce,this);
                windowTaskMessage.Show();
            }
        }
        public void RemoveButtonClick(Task task)
        {
           GliderDataContext gliderDataContext = GliderDataContext.Instance;
           gliderDataContext.Tasks.Remove(task);
           gliderDataContext.SaveChanges();
               
           ItemsControlTask.ItemsSource = null;
            Tasks.Clear();
            ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
            foreach (Task tas in TaskSort)
            {
                if (tas.Login == Login)
                    Tasks.Add(tas);
            }
            ItemsControlTask.ItemsSource = Tasks;
            
        }
        public void AddButtonClick(Task task)
        {
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            gliderDataContext.Tasks.Add(task);
            gliderDataContext.SaveChanges();

            Tasks.Add(task);

        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txt_Search.Text;
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (true == Tasks[i].Tast.ToUpper().StartsWith(filter.ToUpper()) && txt_Search.Text != "")
                {
                    Tasks[i].Done = true;
                    
                }
                else
                {
                    Tasks[i].Done = false;
                }
            }
            ItemsControlTask.ItemsSource = null;
            ItemsControlTask.ItemsSource = Tasks;
        }
    }
}
