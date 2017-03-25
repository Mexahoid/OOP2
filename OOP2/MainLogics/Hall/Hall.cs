using System;
using System.Collections.Generic;
using System.Threading;

namespace OOP2
{
    /// <summary>
    /// Класс зала с банкоматами.
    /// </summary>
    class Hall
    {
        #region Поля

        /// <summary>
        /// Список обработчиков банкоматов.
        /// </summary>
        private List<ATMHandler> _atmList;

        /// <summary>
        /// Список обработчиков клиентов.
        /// </summary>
        private List<ClientHandler> _clientList;

        /// <summary>
        /// Поток вывода данных на форму.
        /// </summary>
        private Thread _dataUpdateThread;

        #endregion

        #region Методы

        /// <summary>
        /// Конструктор зала.
        /// </summary>
        /// <param name="ATMCount">Количество банкоматов.</param>
        public Hall(int ATMCount, Action<string[,]> FormUpdaterMethod)
        {
            _ReloadData += FormUpdaterMethod;
            _atmList = new List<ATMHandler>();
            _clientList = new List<ClientHandler>();
            for (int i = 0; i < ATMCount; i++)
            {
                //Console.WriteLine("Новый банкомат");
                _atmList.Add(new ATMHandler());
                _atmList[_atmList.Count - 1].StartWork();
            }
            Client.hallLink = this;
        }

        #region Обработка клиентов

        /// <summary>
        /// Обработчик события ухода клиента.
        /// </summary>
        /// <param name="Hash">Позиция уходящего клиента.</param>
        private void _NullClientEventHandler(int Hash)
        {
            object locker = new object();
            lock (locker)
                if (_clientList != null)
                {
                    int c = _clientList.Count;
                    for (int i = 0; i < c; i++)
                    {
                        if (_clientList[i].HandlerID == Hash)
                        {
                            _clientList.RemoveAt(i);
                            break;
                        }
                    }
                }
        }

        /// <summary>
        /// Главный метод работы зала. 
        /// Добавляет клиентов через случайные промежутки времени.
        /// </summary>
        /// <param name="param">Контекст синхронизации.</param>
        public void StartClientThread(object param)
        {
            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            while (true)
            {
                //Console.WriteLine("\n=========\nНовый клиент\n=========");
                _clientList.Add(new ClientHandler(_NullClientEventHandler));
                _clientList[_clientList.Count - 1].StartWork();
                int Time = rnd.Next(0, 30);
                _dataUpdateThread = new Thread(DataReload);
                _dataUpdateThread.Start(param);
                Thread.Sleep(1000 * Time);
            }
        }

        /// <summary>
        /// Возвращает ссылку на банкомат с минимальной очередью.
        /// </summary>
        /// <returns>Возвращает экземпляр типа ATM.</returns>
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
                    //Console.WriteLine($"У бабломета {i} стоит {_atmList[i].QueueLength} человек.");
                    //Console.WriteLine($"Бабломет {i} закрыт - {_atmList[i].Atm.Closed}");
                    if (!_atmList[i].Atm.Closed && _atmList[i].QueueLength < min)
                    {
                        //Console.WriteLine("Оп, проверка");
                        min = _atmList[i].QueueLength;
                        j = i;
                    }
                    Thread.Sleep(1000);
                }
                //Console.WriteLine($"\n=====\nВыдана ссылка на {j} автомат");
                return _atmList[j].Atm;
            }
        }

        #endregion

        #region Вывод данных

        /// <summary>
        /// Вечный метод отдельного потока обновления данных на форме.
        /// </summary>
        /// <param name="param">Контекст синхронизации.</param>
        private void DataReload(object param)
        {
            while (true)
            {
                SynchronizationContext sc = (SynchronizationContext)param;
                sc.Send(_ReloadFormData, GetData());
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Обертка над событием обновления формы.
        /// </summary>
        /// <param name="Arr">Массив данных.</param>
        private void _ReloadFormData(object Arr)
        {
            _ReloadData((string[,])Arr);
        }

        /// <summary>
        /// Событие обновления данных на форме.
        /// </summary>
        private event Action<string[,]> _ReloadData;

        /// <summary>
        /// Возвращает массив с данными о банкоматах.
        /// </summary>
        /// <returns>Возвращает двумерный массив строк.</returns>
        private string[,] GetData()
        {
            int C = _atmList.Count;
            string[,] arr = new string[3, C];
            string[] dat;
            for (int i = 0; i < C; i++)
            {
                arr[0, i] = $"Банкомат {i}";
                if (_atmList[i].Atm == null)
                {
                    arr[1, i] = "Банкомат не работает.";
                    arr[2, i] = "Клиентов нет.";
                }
                else
                {
                    dat = _atmList[i].Atm.GetData();
                    arr[1, i] = dat[0];
                    arr[2, i] = dat[1];
                }
            }
            return arr;
        }

        #endregion

        #region Финализатор

        /// <summary>
        /// Деструктор холла. Отключает поток обновления
        /// формы и удаляет всех клиентов и банкоматы.
        /// </summary>
        ~Hall()
        {
            _ReloadData = null;
            _dataUpdateThread.Abort();
            int C = _clientList.Count;
            for (int i = 0; i < C; i++)
                _clientList[i] = null;
            C = _atmList.Count;
            for (int i = 0; i < C; i++)
                _atmList[i] = null;
        }

        #endregion

        #endregion
    }
}