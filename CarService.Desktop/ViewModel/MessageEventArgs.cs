using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Desktop.ViewModel
{
    public class MessageEventArgs : EventArgs
    {
        public String Message { get; private set; }
        public MessageEventArgs(String message) { Message = message; }
    }
}
