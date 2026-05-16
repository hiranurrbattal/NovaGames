namespace NovaGames.Models
{
    public class FreeGame : Game
    {
        public FreeGame()
        {
            Price = 0;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} | Category: {Category} | Free Game");
        }
    }
}