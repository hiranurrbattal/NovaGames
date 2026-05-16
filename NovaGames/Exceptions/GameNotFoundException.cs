namespace NovaGames.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException()
            : base("Game not found!")
        {
        }
    }
}