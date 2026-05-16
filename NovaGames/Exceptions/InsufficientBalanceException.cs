namespace NovaGames.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException()
            : base("Not enough balance!")
        {
        }
    }
}