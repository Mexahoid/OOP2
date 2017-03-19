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
        /// <param name="EventHandler">Делегат для обработки события ухода.</param>
        public Client(Action EventHandler)
        {
            _goOutEvent += EventHandler;
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
        public void Action()
        {
            if (_atmLink != null)
                TakeResponse(_atmLink.OrderMoney(_desire));
            else if (_state == ClientState.Fresh)
            {
                _atmLink = hallLink.GetMinimalATMLink();
                _atmLink.Enqueue(this);
            }
            else
                _goOutEvent();
            Thread.Sleep(1000);
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
