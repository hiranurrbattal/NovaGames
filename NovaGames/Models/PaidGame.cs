namespace NovaGames.Models
{
    public class PaidGame : Game
    {
        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} | Category: {Category} | Paid Game | {Price} TL");
        }
    }
}