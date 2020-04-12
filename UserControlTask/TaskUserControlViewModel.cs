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
using System.Windows.Input;

namespace Glider_WPF_1._0.UserControlTask
{
    
    class TaskUserControlViewModel : ViewModel
    {
        TaskUserControl taskUserControl;
        private string heading;
        private string task;
        private DateTime data = DateTime.Now;
        private DateTime time = DateTime.Now;
        public string Heading 
        {
            get
            {
                return heading;
            }
            set
            {
                Set(ref heading, value);
            }
        }
        public string Task 
        {
            get
            {
                return task;
            }
            set
            {
                Set(ref task, value);
            } 
        }
        public DateTime Data 
        {
            get
            {
                return data;
            }
            set
            {
                Set(ref data, value);
            }
        }
        public DateTime Time 
        {
            get
            {
                return time;
            }
            set
            {
                Set(ref time, value);
            }
        }
        public EventHandler eventTimer;
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ObservableCollection<Task> Tasks
        {
            get
            {
                return tasks;
            }
            
        }
        private ICommand addTask;
        public ICommand AddTask
        {
            get
            {
                return addTask ?? (addTask = new CommandExecutor(() =>
                {
                    try
                    {
                        tasks.Clear();
                        ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                        foreach (Task tas in TaskSort)
                        {
                            if (tas.Login == taskUserControl.Login)
                                tasks.Add(tas);
                        }
                        taskUserControl.ItemsControlTask.ItemsSource = Tasks;
                        DateTime? date = Data;
                        DateTime? time = Time;
                        DateTime dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
                        Task task = new Task(Heading, Task, dateTime, taskUserControl.Login);
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Tasks.Add(task);
                        gliderDataContext.SaveChanges();
                        tasks.Add(task);
                    }
                    catch
                    {

                        MessageBox.Show("Заполните поля");
                    }
                    Heading = "";
                    Task = "";
                    Data = DateTime.Now;
                    Time = DateTime.Now;
                }));
                
            }
            
        }
        private ICommand removeTask;
        public ICommand RemoveTask
        {
            get
            {
                return removeTask ?? (removeTask = new CommandExecutor(() => 
                {
                    try
                    {
                        foreach (Task taskElementUser in taskUserControl.ItemsControlTask.Items.SourceCollection)
                        {
                            if (taskElementUser.Done == true)
                            {
                                GliderDataContext gliderDataContext = GliderDataContext.Instance;
                                gliderDataContext.Tasks.Remove(taskElementUser);
                                gliderDataContext.SaveChanges();
                            }
                        }
                        taskUserControl.ItemsControlTask.ItemsSource = null; 
                        tasks.Clear();
                        ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                        foreach (Task tas in TaskSort)
                        {
                            if (tas.Login == taskUserControl.Login)
                                tasks.Add(tas);
                        }
                        taskUserControl.ItemsControlTask.ItemsSource = Tasks;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(Convert.ToString(ex));
                    }
                }));
            }
        }
        private ICommand induceTask;
        public ICommand InduceTask
        {
            get
            {
                return induceTask ?? (induceTask = new CommandExecutor(() => 
                {
                    Task taskInduce = null;
                    foreach (Task taskElementUser in taskUserControl.ItemsControlTask.Items.SourceCollection)
                    {
                        if (taskElementUser.Done == true)
                        {
                            taskInduce = taskElementUser;
                        }
                    }
                    if (taskInduce != null)
                    {
                        WindowTaskMessage windowTaskMessage = new WindowTaskMessage(taskInduce, taskUserControl);
                        windowTaskMessage.Show();
                    }
                }));
            }
        }
        public TaskUserControlViewModel(TaskUserControl taskUserControl)
        {
            this.taskUserControl = taskUserControl;
            ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
            foreach (Task tas in TaskSort)
            {
                if (tas.Login == taskUserControl.Login)
                    tasks.Add(tas);
            }
            eventTimer = (sender, e) =>
            {
                foreach (Task task in Tasks)
                {
                    DateTime dateTimeAlarm = new DateTime(task.Alarm.Year, task.Alarm.Month, task.Alarm.Day, task.Alarm.Hour, task.Alarm.Minute, 0);
                    DateTime dateTimeNow = DateTime.Now;
                    DateTime dateTimeEqual = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, dateTimeNow.Minute, 0);
                    if (dateTimeAlarm <= dateTimeEqual)
                    {
                        WindowTaskMessage windowTaskMessage = new WindowTaskMessage(task, taskUserControl);
                        windowTaskMessage.Show();

                    }
                }
            };           
        }
    }
}
