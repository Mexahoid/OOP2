using System;
using System.Collections.Generic;
using System.Threading;

namespace OOP2
{
    /// <summary>
    /// Класс банкомата.
    /// </summary>
    class ATM
    {
        #region Поля

        /// <summary>
        /// Минимальное значение суммы в банкомате.
        /// </summary>
        public static int threshold;

        /// <summary>
        /// Закрыт ли банкомат.
        /// </summary>
        private bool _closed;

        /// <summary>
        /// Очередь к этому банкомату.
        /// </summary>
        private Queue<Client> _clientQueue;

        /// <summary>
        /// Событие обновления очереди.
        /// </summary>
        private event Action<bool> _queueUpdEvent;

        /// <summary>
        /// Главный короб с деньгами.
        /// </summary>
        private MoneyBox _mb;

        /// <summary>
        /// Свойство возврата состояния банкомата.
        /// </summary>
        public bool Closed
        {
            get
            {
                return _closed;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Конструктор банкомата.
        /// </summary>
        public ATM(Action<bool> QueueEventHandler)
        {
            _clientQueue = new Queue<Client>();
            _mb = new MoneyBox();
            _closed = false;
            _queueUpdEvent += QueueEventHandler;
        }

        /// <summary>
        /// Заказывает определённую сумму на выдачу.
        /// </summary>
        /// <param name="Value">Сумма выдачи.</param>
        /// <returns>Возвращает код ответа.</returns>
        public ResponseCode OrderMoney(int Value)
        {
            //Console.WriteLine($"Обращение на выдачу: {Value}");
            object Locker = new object();
            lock (Locker)
            {
                Thread.Sleep(1000);

                if (_mb.Cash < threshold)
                {
                    _closed = true;
                    return ResponseCode.Closed;
                }
                int[] MoneyStacksCount = _mb.ReturnMoneyStackCount();
                int[] DesireStacks;

                //Начальное условие, можно ли набрать* всеми доступными купюрами
                if (!GetValueToNoteConversion(Value, out DesireStacks))
                    return ResponseCode.TryToAbs;

                //Если вообще не хватит денег на запрос
                if (_mb.Cash < Value)
                    return ResponseCode.NotEnoughMoney;

                //Иначе продолжаем выполнять
                bool IsAvailable = true;
                int Buffer = 0;
                //Цикл просмотра допустимости набора присутствующими купюрами
                int[] Nominals = new int[] { 10, 50, 100, 500, 1000, 5000 };
                for (int i = 0; i < 6; i++)
                    if (MoneyStacksCount[i] < DesireStacks[i])
                    {
                        IsAvailable = false;
                        if (i != 5)
                        {
                            //Разбиваем верхний номинал на N кол-во нижних и вычитаем
                            DesireStacks[i + 1] += (DesireStacks[i] - MoneyStacksCount[i]) *
                             Nominals[5 - i] / Nominals[4 - i];

                            Buffer += (DesireStacks[i] - MoneyStacksCount[i]) *
                              Nominals[5 - i] % Nominals[4 - i];
                            if (Buffer != 0)
                            {
                                int Temp = Buffer % Nominals[4 - i];
                                if (Temp == 0)
                                {
                                    DesireStacks[i + 1] += Buffer / Nominals[4 - i];
                                    Buffer = 0;
                                }
                            }
                            DesireStacks[i] = MoneyStacksCount[i];
                        }
                        else
                            break;
                    }
                    else
                        IsAvailable = true;

                //Если можно набрать - вытащить сумму из пачек
                if (IsAvailable)
                {
                    for (int i = 0; i < 6; i++)
                        _mb.GetMoneyFromStack(i, DesireStacks[i]);
                    return ResponseCode.Good;
                }
                else
                    return ResponseCode.TryToAbs;
            }

        }

        /// <summary>
        /// Рассчитывает выдаваемую сумму через вхождения номиналов.
        /// </summary>
        /// <param name="Value">Нужная сумма.</param>
        /// <param name="Counts">Вхождения купюр.</param>
        /// <returns></returns>
        private bool GetValueToNoteConversion(int Value, out int[] Counts)
        {
            /*[10, 50, 100, 500, 1000, 5000]*/
            Counts = new int[6];
            bool flag = false;
            for (int i = 5, j = 5000; i >= 0 && Value != 0; i--, flag = !flag, j /= flag ? 2 : 5)
            {
                Counts[5 - i] = Value / j;
                Value %= j;
            }
            return Value == 0;
        }

        /// <summary>
        /// Метод для выдачи состояния текстом. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _closed ? "Закрыт." : _mb.Cash.ToString();
        }

        /// <summary>
        /// Вечный метод обработки клиента в голове очереди.
        /// </summary>
        public void ServeClient()
        {
            while (true)
            {
                if (_clientQueue.Count != 0)
                {
                    //Console.WriteLine("Очередь не пуста");
                    Client cl = _clientQueue.Peek();
                    cl.GetServed();
                    if (cl.State == ClientState.Bad || cl.State == ClientState.Good)
                    {
                        //Console.WriteLine("Выгоняю клиента");
                        _clientQueue.Dequeue();
                        _queueUpdEvent(false);
                        cl.GoOut();
                    }
                }
                Thread.Sleep(50);  //Пусть считается, как время отклика
            }
        }

        /// <summary>
        /// Отвечает клиенту True, если клиент находится в начале очереди.
        /// (Выполняется в потоке клиента).
        /// </summary>
        /// <param name="Asker">Экземпляр спрашивающего клиента.</param>
        /// <returns>Возвращает логическое значение.</returns>
        public bool AnswerClient(Client Asker)
        {
            //Console.WriteLine("\n=====\nОтвет клиенту\n=====\n");
            return Asker.Equals(_clientQueue.Peek());
        }

        /// <summary>
        /// Возвращает данные о балансе и очереди.
        /// </summary>
        /// <returns>Возвращает массив строк.</returns>
        public string[] GetData()
        {
            string[] arr = new string[2];
            arr[0] = "Деньги: " + _mb.Cash;
            arr[1] = "Клиенты: " + _clientQueue.Count;
            return arr;
        }

        /// <summary>
        /// Заталкивает клиента в очередь.
        /// </summary>
        /// <param name="cl">Экземпляр заталкиваемого клиента.</param>
        public void Enqueue(Client cl)
        {
            object locker = new object();

            lock (locker)
            {
                //Console.WriteLine("\n=====\nКлиент зашел в очередь\n=====\n");
                _clientQueue.Enqueue(cl);
                _queueUpdEvent(true);
            }
        }

        #endregion
    }
}
