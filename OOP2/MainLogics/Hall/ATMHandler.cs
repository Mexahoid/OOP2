using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP2
{
    class ATMHandler
    {
        private ATM _atm;

        private Thread _thread;

        private int _queueLength;

        public ATMHandler()
        {
            _atm = new ATM(_QueueLengthChangeEventHandler);
            _thread = new Thread(_atm.ServeClient);
            _queueLength = 0;
        }

        public void StartWork()
        {
            _thread.Start();
        }

        ~ATMHandler()
        {
            _thread.Abort();
            _atm = null;
        }

        public void GetDrawingEvent()
        {

        }

        public int QueueLength
        {
            get
            {
                return _queueLength;
            }
        }

        private void _QueueLengthChangeEventHandler(bool flag)
        {
            Console.WriteLine("Вызов обработчика очереди");
            if (flag)
            {
                _queueLength++;
                Console.WriteLine("Добавлен");
            }
            else
            {
                _queueLength--;
                Console.WriteLine("Убран");
            }
        }

        public ATM Atm
        {
            get
            {
                return _atm;
            }
        }
    }
}
