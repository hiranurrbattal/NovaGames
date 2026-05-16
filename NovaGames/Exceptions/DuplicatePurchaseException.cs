namespace NovaGames.Exceptions
{
    public class DuplicatePurchaseException : Exception
    {
        public DuplicatePurchaseException()
            : base("You already own this game!")
        {
        }
    }
}