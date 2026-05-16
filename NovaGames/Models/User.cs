namespace NovaGames.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 1000;

        public List<string> Achievements { get; set; } = new List<string>();
        public List<string> Library { get; set; } = new List<string>();
        public void ShowProfile()
        {
            Achievements ??= new List<string>();
            Library ??= new List<string>();

            Console.WriteLine("=== PROFILE ===");
            Console.WriteLine("Username: " + Username);
            Console.WriteLine("Balance: " + Balance);

            Console.WriteLine("Library:");
            if (Library.Count == 0)
                Console.WriteLine("No games yet.");
            else
                foreach (var g in Library)
                    Console.WriteLine("- " + g);

            Console.WriteLine("\nAchievements:");
            if (Achievements.Count == 0)
                Console.WriteLine("No achievements yet.");
            else
                foreach (var a in Achievements)
                    Console.WriteLine("* " + a);
        }
        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Balance amount must be greater than zero!");

            Balance += amount;
        }
    }
}