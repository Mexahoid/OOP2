using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    /// <summary>
    /// Состояния клиента.
    /// </summary>
    public enum ClientState
    {
        /// <summary>
        /// Деньги получены.
        /// </summary>
        Good,

        /// <summary>
        /// Клиент обдумывает выбор.
        /// </summary>
        Thinking,

        /// <summary>
        /// Клиент не получил деньги.
        /// </summary>
        Bad
    }
}
