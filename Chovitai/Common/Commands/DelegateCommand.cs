using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

#pragma warning disable CS8612
#pragma warning disable CS8625
#pragma warning disable CS8618
#pragma warning disable CS8767

namespace Chovitai.Common.Commands
{
    /// <summary>
    /// 引数を受け付けない（引数なし）DelegateCommand
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action action;
        private readonly Func<bool> canExecute;
        public DelegateCommand(Action action, Func<bool> canExecute = default)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }

        public void DelegateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// 任意の型の引数を1つ受け付けるDelegateCommand
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> _action;
        private readonly Func<T, bool> _canExecute;
        public DelegateCommand(Action<T> action, Func<T, bool> canExecute = default)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke((T)parameter);
        }

        public void DelegateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

#pragma warning restore CS8612
#pragma warning restore CS8625
#pragma warning restore CS8618
#pragma warning restore CS8767
