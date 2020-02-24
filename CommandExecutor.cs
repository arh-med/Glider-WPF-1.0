using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Glider_WPF_1._0
{
    class CommandExecutor: ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action actionToExecute;
        
        public CommandExecutor(Action actionToExecute)
        {
            this.actionToExecute = actionToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.actionToExecute();
        }
    }
}
