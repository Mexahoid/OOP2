using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    /// <summary>
    /// Класс клиента.
    /// </summary>
    class Client
    {
        /// <summary>
        /// Минимальный заказ.
        /// </summary>
        public static int lb;

        /// <summary>
        /// Максимальный заказ.
        /// </summary>
        public static int rb;

        /// <summary>
        /// Желание клиента.
        /// </summary>
        private int _desire;

        private int _tries;

        private ClientState _state;

        private ATM _atmLink;

        public Client(ATM Link)
        {
            _atmLink = Link;

            Random Rnd = new Random(DateTime.UtcNow.Millisecond);

            _desire = Rnd.Next(lb, rb);
        }

        public ClientState State { get => _state; set => _state = value; }

        /// <summary>
        /// Главное действие клиента.
        /// </summary>
        public void Action()
        {
            TakeResponse(_atmLink.OrderMoney(_desire));
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
                        _state = ClientState.Bad;
                    break;
                case ResponseCode.Closed:
                    _tries = 0;
                    _state = ClientState.Bad;
                    break;
            }
        }
    }
}
