using System;
using System.Threading;

namespace OOP2
{
    /// <summary>
    /// Класс клиента.
    /// </summary>
    class Client
    {
        #region Поля

        public static Hall hallLink;

        /// <summary>
        /// Минимальный заказ.
        /// </summary>
        public static int lb;

        /// <summary>
        /// Максимальный заказ.
        /// </summary>
        public static int rb;

        private event Action _goOutEvent;

        /// <summary>
        /// Желание клиента.
        /// </summary>
        private int _desire;

        /// <summary>
        /// Количество доступных клиенту попыток.
        /// </summary>
        private int _tries;

        /// <summary>
        /// Состояние клиента.
        /// </summary>
        private ClientState _state;

        /// <summary>
        /// Ссылка на используемый банкомат.
        /// </summary>
        private ATM _atmLink;

        /// <summary>
        /// Состояние клиента.
        /// </summary>
        public ClientState State { get { return _state; } }

        #endregion

        #region Методы

        /// <summary>
        /// Конструктор клиента.
        /// </summary>
        /// <param name="GoOutEventHandler">Делегат для обработки события ухода.</param>
        public Client(Action GoOutEventHandler)
        {
            _goOutEvent += GoOutEventHandler;
            Random Rnd = new Random(DateTime.UtcNow.Millisecond);
            _state = ClientState.Fresh;
            _desire = Rnd.Next(lb, rb);
            _tries = 3;
        }

        /// <summary>
        /// Главное действие клиента.
        /// </summary>
        public void AskATM()
        {
            while (_atmLink == null)
            {
                Thread.Sleep(1000);
                if (_state == ClientState.Fresh)
                {
                    _atmLink = hallLink.GetMinimalATMLink();
                    //Console.WriteLine("Клиент взял ссылку");
                    _atmLink.Enqueue(this);
                }
            }
            while (_state == ClientState.Fresh)
            {
                //Console.WriteLine("Попытка захода в очередь");
                if (!_atmLink.AnswerClient(this))
                {
                    //Console.WriteLine("Не зашел");
                    Thread.Sleep(5000);
                }
                else
                {
                    //Console.WriteLine("\n=====\nЗашел\n=====\n");
                    break;
                }
            }
        }

        /// <summary>
        /// Метод обслуживание клиента (выполняется в потоке банкомата).
        /// </summary>
        public void GetServed()
        {
            object locker = new object();

            lock (locker)
            {
                //Console.WriteLine("Банкомат обслуживает меня");
                TakeResponse(_atmLink.OrderMoney(_desire));
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Насильственный выгон клиента из очереди.
        /// (Вызывается банкоматом).
        /// </summary>
        public void GoOut()
        {
            //Console.WriteLine("\n=====\nКлиент ушел\n=====\n");
            _goOutEvent();
        }

        /// <summary>
        /// Определяет действия клиента при получении кода от банкомата.
        /// </summary>
        /// <param name="Code">Код ответа.</param>
        private void TakeResponse(ResponseCode Code)
        {
            switch (Code)
            {
                case ResponseCode.Good:
                    //Console.WriteLine("Еее, я получил бабло");
                    _state = ClientState.Good;
                    break;
                case ResponseCode.TryToAbs:
                    _tries--;
                    //Console.WriteLine("Надо подумать");
                    if (_tries == 2)
                    {
                        //Console.WriteLine($"Хочу {_desire}");
                        _desire /= 100;
                        _desire *= 100;
                    }
                    else if (_tries == 1)
                    {
                        //Console.WriteLine($"Хочу {_desire}");
                        _desire /= 1000;
                        _desire *= 1000;
                    }
                    //Console.WriteLine($"Хочу {_desire}");
                    _state = ClientState.Thinking;
                    break;
                case ResponseCode.NotEnoughMoney:
                    _tries--;
                    //Console.WriteLine("Чорт, денег не хватает");
                    if (_tries != 0)
                    {
                        Random Rnd = new Random(DateTime.UtcNow.Millisecond);

                        _desire = Rnd.Next(lb, rb);
                        _state = ClientState.Thinking;
                    }
                    else
                        _state = ClientState.Bad;
                    break;
                case ResponseCode.Closed:
                    _tries = 0;
                    //Console.WriteLine("Эх, ухожу");
                    _state = ClientState.Bad;
                    break;
            }
            //_Destructor();  //<- Не помню, зачем нужно.
        }
        #endregion
    }
}
