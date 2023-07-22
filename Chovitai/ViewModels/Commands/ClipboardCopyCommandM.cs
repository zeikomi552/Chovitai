using Chovitai.Common.Commands;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.ViewModels.Comands
{
    public class ClipboardCopyCommandM : ViewModelBase
    {
        public ClipboardCopyCommandM()
        {
            ClipboardCopy = new ClipboardCopyCommand();
        }
        public ClipboardCopyCommand ClipboardCopy { get; private set; }

        public override void Init(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        public override void Close(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
