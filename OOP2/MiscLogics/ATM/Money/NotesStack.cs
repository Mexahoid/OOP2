using System;
using System.Collections.Generic;
namespace OOP2
{
    /// <summary>
    /// Класс пачки денег.
    /// </summary>
    public class NotesStack
    {
        /// <summary>
        /// Номинал купюр в пачке.
        /// </summary>
        private int _nominal;

        /// <summary>
        /// Список купюр.
        /// </summary>
        private List<Note> _notes;

        /// <summary>
        /// Возвращает номинал пачки.
        /// </summary>
        public int Nominal { get { return _nominal; } }

        /// <summary>
        /// Конструктор пачки денег.
        /// </summary>
        /// <param name="Nominal">Номинал купюр.</param>
        public NotesStack(int Nominal)
        {
            _nominal = Nominal;
            _notes = new List<Note>();
            Random Rnd = new Random(DateTime.Now.Millisecond);
            int Rand = Rnd.Next(0, 100);
            for (int i = 0; i < Rand; i++)
                _notes.Add(new Note(this.Nominal));
        }

        /// <summary>
        /// Вытаскивает несколько купюр из пачки.
        /// </summary>
        /// <param name="Count">Количество вытаскиваемых купюр.</param>
        public void TakeMoneyFromStack(int Count)
        {
            for (int i = 0; i < Count; i++)
                _PopNote();
        }
        
        /// <summary>
        /// Возвращает общую сумму купюр в пачке.
        /// </summary>
        /// <returns></returns>
        public int ReturnStackCash() { return _nominal * _notes.Count;}

        /// <summary>
        /// Возвращает количество купюр в пачке.
        /// </summary>
        /// <returns></returns>
        public int ReturnStackCount() { return _notes.Count;}

        /// <summary>
        /// Убирает одну купюру из пачки.
        /// </summary>
        private void _PopNote() { _notes.RemoveAt(_notes.Count - 1);}
    }
}
