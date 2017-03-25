using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OOP2
{
    /// <summary>
    /// Класс зала с банкоматами.
    /// </summary>
    class Hall
    {
        /// <summary>
        /// Список банкоматов.
        /// </summary>
        private List<ATMHandler> _atmList;

        private List<ClientHandler> _clientList;

        private object _locker;

        /// <summary>
        /// Конструктор зала.
        /// </summary>
        /// <param name="ATMCount">Количество банкоматов.</param>
        public Hall(int ATMCount)
        {
            _locker = new object();
            _atmList = new List<ATMHandler>();
            _clientList = new List<ClientHandler>();
            for (int i = 0; i < ATMCount; i++)
            {
                Console.WriteLine("Новый банкомат");
                _atmList.Add(new ATMHandler());
                _atmList[_atmList.Count - 1].StartWork();
            }
            Client.hallLink = this;
        }

        private void _NullClientEventHandler(int Pos)
        {
            lock (_locker)
                if (_clientList != null)
                    _clientList.RemoveAt(Pos);
        }

        public void StartClientThread()
        {
            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            while (true)
            {
                Console.WriteLine("Новый клиент");
                _clientList.Add(new ClientHandler(_NullClientEventHandler, _clientList.Count));
                _clientList[_clientList.Count - 1].StartWork();
                int Time = rnd.Next(0, 30);
                Thread.Sleep(1000 * Time);
            }
        }

        public ATM GetMinimalATMLink()
        {
            object locker = new object();
            lock (locker)
            {
                int C = _atmList.Count;
                int min = int.MaxValue;
                int j = 0;
                for (int i = 0; i < C; i++)
                {
                    if (!_atmList[i].Atm.Closed && _atmList[i].QueueLength < min)
                    {
                        min = _atmList[i].QueueLength;
                        j = i;
                    }
                }
                return _atmList[j].Atm;
            }
        }

        ~Hall()
        {
            int C = _clientList.Count;
            for (int i = 0; i < C; i++)
                _clientList[i] = null;
            C = _atmList.Count;
            for (int i = 0; i < C; i++)
                _atmList[i] = null;
        }
    }
}
