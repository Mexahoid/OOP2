﻿namespace OOP2
{
    /// <summary>
    /// Купюра.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Номинал купюры.
        /// </summary>
        private int _nominal;

        /// <summary>
        /// Конструктор купюры.
        /// </summary>
        /// <param name="Nominal">Номинал купюры.</param>
        public Note(int Nominal)
        {
            _nominal = Nominal;
        }

        /// <summary>
        /// Номинал купюры.
        /// </summary>
        public int Nominal { get { return _nominal; } }
    }
}
