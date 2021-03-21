using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendIt_
{
    public interface iChatConnector
    {
        void sendMessage(String message);
        void connect();

    }
}
