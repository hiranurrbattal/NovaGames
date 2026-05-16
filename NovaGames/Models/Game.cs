namespace NovaGames.Models
{
    public class Game
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public double TotalRating { get; set; } = 0;
        public int RatingCount { get; set; } = 0;

        public double AverageRating
        {
            get
            {
                if (RatingCount == 0)
                    return 0;

                return TotalRating / RatingCount;
            }
        }

        public virtual void ShowInfo()
        {
            string ratingText = RatingCount == 0
                ? "No rating"
                : $"Rating: {AverageRating:F1}/10";

            if (Price == 0)
            {
                Console.WriteLine($"{Name} | Category: {Category} | Free Game | {ratingText}");
            }
            else
            {
                Console.WriteLine($"{Name} | Category: {Category} | Paid Game | Price: {Price} TL | {ratingText}");
            }
        }
    }
}