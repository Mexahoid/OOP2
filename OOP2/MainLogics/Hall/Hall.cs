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

        private Thread _dataUpdateThread;

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
                if (_clientList != null && _clientList.Count > Pos)
                    _clientList.RemoveAt(Pos);
        }

        public void StartClientThread(object param)
        {
            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            while (true)
            {
                Console.WriteLine("\n=========\nНовый клиент\n=========");
                _clientList.Add(new ClientHandler(_NullClientEventHandler, _clientList.Count));
                _clientList[_clientList.Count - 1].StartWork();
                int Time = rnd.Next(0, 30);
                _dataUpdateThread = new Thread(DataReload);
                _dataUpdateThread.Start(param);
                Thread.Sleep(1000 * Time);
            }
        }

        private void DataReload(object param)
        {
            while (true)
            {
                SynchronizationContext sc = (SynchronizationContext)param;
                sc.Send(Sas, GetData());
                Thread.Sleep(10);
            }
        }

        public void Sas(object Arr)
        {
            if (ReloadData != null)
                ReloadData((string[,])Arr);
        }

        public event Action<string[,]> ReloadData;

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
                    Console.WriteLine($"У бабломета {i} стоит {_atmList[i].QueueLength} человек.");
                    Console.WriteLine($"Бабломет {i} закрыт - {_atmList[i].Atm.Closed}");
                    if (!_atmList[i].Atm.Closed && _atmList[i].QueueLength < min)
                    {
                        Console.WriteLine("Оп, проверка");
                        min = _atmList[i].QueueLength;
                        j = i;
                    }
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"\n=====\nВыдана ссылка на {j} автомат");
                return _atmList[j].Atm;
            }
        }

        private string[,] GetData()
        {
            string[,] arr = new string[3, 3];
            string[] dat;
            for (int i = 0; i < 3; i++)
            {
                arr[0, i] = $"Бабломет {i}";
                dat = _atmList[i].Atm.GetData();
                arr[1, i] = dat[0];
                arr[2, i] = dat[1];
            }
            return arr;
        }

        ~Hall()
        {
            _dataUpdateThread.Abort();
            int C = _clientList.Count;
            for (int i = 0; i < C; i++)
                _clientList[i] = null;
            C = _atmList.Count;
            for (int i = 0; i < C; i++)
                _atmList[i] = null;
        }
    }
}
