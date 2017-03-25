using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2
{
    /// <summary>
    /// Класс-обработчик клиента.
    /// </summary>
    class ClientHandler
    {
        #region Поля

        /// <summary>
        /// Экземпляр клиента.
        /// </summary>
        private Client _client;

        /// <summary>
        /// Экземпляр потока.
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// Событие ухода клиента.
        /// </summary>
        private event Action<int> _nullClientEvent;

        /// <summary>
        /// Хэш обработчика.
        /// </summary>
        private int _handlerID;

        /// <summary>
        /// Свойство выдачи хэша обработчика.
        /// </summary>
        public int HandlerID { get { return _handlerID; } }

        #endregion

        #region Методы

        /// <summary>
        /// Конструктор обработчика клиента.
        /// </summary>
        /// <param name="Event">Метод-обработчик события ухода клиента.</param>
        public ClientHandler(Action<int> Event)
        {
            _nullClientEvent += Event;
            _handlerID = GetHashCode();
            _client = new Client(_ClientGoneEventHandler);
            _thread = new Thread(_client.AskATM);
        }

        /// <summary>
        /// Запускает поток клиента.
        /// </summary>
        public void StartWork()
        {
            _thread.Start();
        }

        /// <summary>
        /// Собственный обработчик ухода клиента.
        /// </summary>
        private void _ClientGoneEventHandler()
        {
            _thread.Abort();
            _client = null;
            _nullClientEvent(HandlerID);
        }

        /// <summary>
        /// Деструктор обработчика клиента. Отключает поток и удаляет клиента.
        /// </summary>
        ~ClientHandler()
        {
            _ClientGoneEventHandler();
        }

        #endregion
    }
}
