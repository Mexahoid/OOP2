using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{

    /// <summary>
    /// Главный короб пачек с деньгами.
    /// </summary>
    public class MoneyBox
    {
        /// <summary>
        /// Список пачек денег.
        /// </summary>
        private List<NotesStack> _money;

        /// <summary>
        /// Общая сумма купюр в коробе.
        /// </summary>
        private int _cash;

        /// <summary>
        /// Общая сумма купюр.
        /// </summary>
        public int Cash { get => _cash; set => _cash = value; }

        /// <summary>
        /// Конструктор короба.
        /// </summary>
        public MoneyBox()
        {
            bool flag = false;
            _cash = 0;
            for (int i = 10; i <= 5000; i *= flag ? 5 : 2, flag = !flag)
            {
                _money.Add(new NotesStack(i));
                _cash += _money[_money.Count - 1].ReturnStackCash();
            }
        }
        
        /// <summary>
        /// Возвращает всю сумму в банкомате.
        /// </summary>
        /// <returns></returns>
        private void _RecountCash()
        {
            _cash = 0;
            for (int i = 0; i < 6; i++)
            {
                _cash += _money[i].ReturnStackCash();
            }
        }

        /// <summary>
        /// Возвращает массив количеств купюр по пачкам.
        /// </summary>
        /// <returns></returns>
        public int[] ReturnMoneyStackCount()
        {
            int[] MoneyStacksCount = new int[6];
            for (int i = 0; i < 6; i++)
            {
                MoneyStacksCount[i] = _money[i].ReturnStackCount();
            }
            return MoneyStacksCount;
        }

        /// <summary>
        /// Вытаскивает из пачки определённое количество денег.
        /// </summary>
        /// <param name="Stack"></param>
        /// <param name="Count"></param>
        public void GetMoneyFromStack(int Stack, int Count)
        {
            _money[Stack].TakeMoneyFromStack(Count);
            _RecountCash();
        }
    }
}
