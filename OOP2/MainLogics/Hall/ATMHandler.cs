using System.Threading;

namespace OOP2
{
    /// <summary>
    /// Обработчик банкомата.
    /// </summary>
    class ATMHandler
    {
        #region Поля

        /// <summary>
        /// Экземпляр банкомата.
        /// </summary>
        private ATM _atm;

        /// <summary>
        /// Поток банкомата.
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// Длина очереди к банкомату.
        /// </summary>
        private int _queueLength;

        /// <summary>
        /// Свойство выдачи длины очереди.
        /// </summary>
        public int QueueLength
        {
            get
            {
                return _queueLength;
            }
        }
        
        #endregion

        #region Методы

        /// <summary>
        /// Конструктор обработчика банкомата.
        /// </summary>
        public ATMHandler()
        {
            _atm = new ATM(_QueueLengthChangeEventHandler);
            _thread = new Thread(_atm.ServeClient);
            _queueLength = 0;
        }

        /// <summary>
        /// Метод запуска потока работы
        /// </summary>
        public void StartWork()
        {
            _thread.Start();
        }

        /// <summary>
        /// Обработчик изменения состония очереди.
        /// </summary>
        /// <param name="flag">Добавить - True, убрать - False</param>
        private void _QueueLengthChangeEventHandler(bool flag)
        {
            //Console.WriteLine("Вызов обработчика очереди");
            if (flag)
            {
                _queueLength++;
                //Console.WriteLine("Добавлен");
            }
            else
            {
                _queueLength--;
                //Console.WriteLine("Убран");
            }
        }

        /// <summary>
        /// Свойство выдачи ссылки на банкомат.
        /// </summary>
        public ATM Atm
        {
            get
            {
                return _atm;
            }
        }

        /// <summary>
        /// Деструктор обработчика. Отключает поток выполнения.
        /// </summary>
        ~ATMHandler()
        {
            _thread.Abort();
            _atm = null;
        }

        #endregion
    }
}
