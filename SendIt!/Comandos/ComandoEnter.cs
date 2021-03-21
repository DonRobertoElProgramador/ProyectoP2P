using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SendIt_.Comandos
{
    public class ComandoEnter : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action execute;

        public ComandoEnter(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
