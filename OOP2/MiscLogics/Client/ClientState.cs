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
        Bad,

        /// <summary>
        /// Только пришел.
        /// </summary>
        Fresh
    }
}
