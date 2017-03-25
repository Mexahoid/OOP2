namespace OOP2
{
    /// <summary>
    /// Перечислимый тип кодов ответа.
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// Операция прошла успешно.
        /// </summary>
        Good = 200,

        /// <summary>
        /// Ещё раз, только округлить.
        /// </summary>
        TryToAbs = 451,

        /// <summary>
        /// В банкомате не хватает денег.
        /// </summary>
        NotEnoughMoney = 1984, 

        /// <summary>
        /// Банкомат не работает.
        /// </summary>
        Closed = 2007
    };
}
