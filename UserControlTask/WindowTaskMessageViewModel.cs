using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glider_WPF_1._0.UserControlTask
{
    class WindowTaskMessageViewModel:ViewModel
    {
        WindowTaskMessage windowTaskMessage;
        TaskUserControl taskUserControl;
        private string heading;
        private string taskProperti;
        private DateTime data = DateTime.Now;
        private DateTime time = DateTime.Now;
        private string login;
       
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
                return taskProperti;
            }
            set
            {
                Set(ref taskProperti, value);
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
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ObservableCollection<Task> Tasks
        {
            get
            {
                return tasks;
            }
           
            
        }
        private ICommand doneTask;
        public ICommand DoneTask
        {
            get
            {
                return doneTask ?? (doneTask = new CommandExecutor(() => 
                {
                    Task taskWindow = null;
                    foreach (Task item in Tasks)
                    {
                        taskWindow = item;
                    }
                    #region TaskUserControl
                    GliderDataContext gliderDataContext = GliderDataContext.Instance;
                    gliderDataContext.Tasks.Remove(taskWindow);
                    gliderDataContext.SaveChanges();

                    taskUserControl.ItemsControlTask.ItemsSource = null;
                    tasks.Clear();
                    ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                    foreach (Task tas in TaskSort)
                    {
                    if (tas.Login == taskUserControl.Login)
                            Tasks.Add(tas);
                    }
                    taskUserControl.ItemsControlTask.ItemsSource = Tasks;
                    #endregion 
                    taskUserControl.timer.Start();
                    windowTaskMessage.Close();
                }));
            }
        }
        private ICommand renameTask;
        public ICommand RenameTask
        {
            get
            {
                return renameTask ?? (renameTask = new CommandExecutor(() => 
                {
                    try
                    {
                        Task taskWindow = null;
                        foreach (Task item in Tasks)
                        {
                            taskWindow = item;
                        }
                        #region TaskUserControl
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Tasks.Remove(taskWindow);
                        gliderDataContext.SaveChanges();
                        #endregion
                        DateTime? date = Data;
                        DateTime? time = Time;
                        DateTime dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);
                        Task task = new Task(Heading, Task, dateTime, login);
                        gliderDataContext.Tasks.Add(task);
                        gliderDataContext.SaveChanges();
                        tasks.Add(task);
                        taskUserControl.ItemsControlTask.ItemsSource = null;
                        tasks.Clear();
                        ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                        foreach (Task tas in TaskSort)
                        {
                            if (tas.Login == taskUserControl.Login)
                                tasks.Add(tas);
                        }
                        taskUserControl.ItemsControlTask.ItemsSource = Tasks;
                        taskUserControl.timer.Start();
                        windowTaskMessage.Close();
                    }
                    catch 
                    {

                        
                    }
                }));
            }
        }
        public WindowTaskMessageViewModel(Task task, TaskUserControl taskUserControl, WindowTaskMessage windowTaskMessage)
        {
            this.windowTaskMessage = windowTaskMessage;
            this.taskUserControl = taskUserControl;
            login = taskUserControl.Login;
            tasks.Add(task);
            heading = task.Heading;
            taskProperti = task.Tast;
            data = task.Alarm;
            time = task.Alarm;
            taskUserControl.timer.Stop();
        }
    }
}
