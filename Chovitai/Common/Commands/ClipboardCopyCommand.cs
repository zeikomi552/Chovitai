using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chovitai.Common.Commands
{
    public class ClipboardCopyCommand : ICommand
    {
        // 以下、ICommand用のプロパティ
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) { return true; }

        public void Execute(object? parameter)
        {
            try
            {
                if (parameter != null)
                {
                    Clipboard.SetText(parameter.ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
    }
}
