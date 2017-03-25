using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2
{
    class ClientHandler
    {
        private Client _client;

        private Thread _thread;

        private event Action<int> _nullClientEvent;

        private int _handlerID;

        public ClientHandler(Action<int> Event, int LogNum)
        {
            _nullClientEvent += Event;
            _handlerID = LogNum;
            _client = new Client(_ClientGoneEventHandler);
            _thread = new Thread(_client.AskATM);
        }

        public void StartWork()
        {
            _thread.Start();
        }

        private void _ClientGoneEventHandler()
        {
            _thread.Abort();
            _client = null;
            _nullClientEvent(_handlerID);
        }
        
        ~ClientHandler()
        {
            _ClientGoneEventHandler();
        }
    }
}
