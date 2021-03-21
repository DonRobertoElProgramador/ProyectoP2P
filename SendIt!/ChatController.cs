using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SendIt_
{
    class ChatController : iController
    {

        public ObservableCollection<Message> messages = new ObservableCollection<Message>();        


        public void addMessage(Message mess)
        {
            messages.Add(mess); ;
        }

        public ObservableCollection<Message> getMessages()
        {
            return messages;
        }
    }
}
