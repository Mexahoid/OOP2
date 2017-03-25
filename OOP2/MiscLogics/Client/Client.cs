using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2
{
    /// <summary>
    /// Класс клиента.
    /// </summary>
    class Client
    {
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
        /// Конструктор клиента.
        /// </summary>
        /// <param name="GoOutEventHandler">Делегат для обработки события ухода.</param>
        public Client(Action GoOutEventHandler)
        {
            _goOutEvent += GoOutEventHandler;
            _goOutEvent += _Destructor;
            Random Rnd = new Random(DateTime.UtcNow.Millisecond);
            _state = ClientState.Fresh;
            _desire = Rnd.Next(lb, rb);
        }

        /// <summary>
        /// Состояние клиента.
        /// </summary>
        public ClientState State { get { return _state; } set { _state = value; } }

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
                    Console.WriteLine("Клиент взял ссылку");
                    _atmLink.Enqueue(this);
                }
            }
            while (_state == ClientState.Fresh)
            {
                Console.WriteLine("Попытка захода в очередь");
                if (!_atmLink.AnswerClient(this))
                {
                    Console.WriteLine("Не зашел");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Зашел");
                    break;
                }
            }
        }

        public void GetServed()
        {
            object locker = new object();

            lock (locker)
            {
                Console.WriteLine("Банкомат обслуживает меня");
                TakeResponse(_atmLink.OrderMoney(_desire));
                Thread.Sleep(2000);
            }
        }

        public void GoOut()
        {
            Console.WriteLine("Клиент ушел");
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
                    _state = ClientState.Good;
                    _atmLink = null;
                    break;
                case ResponseCode.TryToAbs:
                    _tries--;
                    if (_tries == 2)
                    {
                        _desire /= 100;
                        _desire *= 100;
                    }
                    else if (_tries == 1)
                    {
                        _desire /= 1000;
                        _desire *= 1000;
                    }
                    _state = ClientState.Thinking;
                    break;
                case ResponseCode.NotEnoughMoney:
                    _tries--;
                    if (_tries != 0)
                    {
                        Random Rnd = new Random(DateTime.UtcNow.Millisecond);

                        _desire = Rnd.Next(lb, rb);
                        _state = ClientState.Thinking;
                    }
                    else
                    {
                        _atmLink = null;
                        _state = ClientState.Bad;
                    }
                    break;
                case ResponseCode.Closed:
                    _tries = 0;
                    _state = ClientState.Bad;
                    _atmLink = null;
                    break;
            }
        }

        private void _Destructor()
        {
            _atmLink = null;
        }
    }
}
