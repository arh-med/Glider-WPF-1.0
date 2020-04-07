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

        Task task = new Task();
        string Heading { get; set; }
        string Task { get; set; }
        DateTime Data { get; set; }
        DateTime Time { get; set; }
        private List<Task> tasks = new List<Task>();
        public List<Task> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                Tasks.Add(task);
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
                    Tasks.Clear();
                    ObservableCollection<Task> TaskSort = new ObservableCollection<Task>(GliderDataContext.Instance.Tasks.ToList());
                    foreach (Task tas in TaskSort)
                    {
                    if (tas.Login == taskUserControl.Login)
                            Tasks.Add(tas);
                    }
                    taskUserControl.ItemsControlTask.ItemsSource = Tasks;
                    #endregion 
                    windowTaskMessage.Close();
                }));
            }
        }
        public WindowTaskMessageViewModel(Task task, TaskUserControl taskUserControl, WindowTaskMessage windowTaskMessage)
        {
            this.task = task;
            this.windowTaskMessage = windowTaskMessage;
            this.taskUserControl = taskUserControl;
        }
    }
}
