using NovaGames.Models;
using NovaGames.Services;

namespace NovaGames.Managers
{
    public class GameStore
    {
        private const string PATH = "games.json";

        public List<Game> Games { get; set; }

        public GameStore()
        {
            Games = JsonService.Load<List<Game>>(PATH);

            if (Games == null)
                Games = new List<Game>();
        }

        public void AddGame(Game game)
        {
            Games.Add(game);
            Save();
        }

        public void RemoveGame(string name)
        {
            var game = GetGame(name);

            if (game != null)
            {
                Games.Remove(game);
                Save();
            }
        }

        public void UpdateGame(string oldName, Game updatedGame)
        {
            var game = GetGame(oldName);

            if (game != null)
            {
                game.Name = updatedGame.Name;
                game.Category = updatedGame.Category;
                game.Price = updatedGame.Price;
                Save();
            }
        }

        public void RateGame(string gameName, double rating)
        {
            var game = GetGame(gameName);

            if (game != null)
            {
                game.TotalRating += rating;
                game.RatingCount++;

                Save();
            }
        }

        public Game GetGame(string name)
        {
            return Games.FirstOrDefault(x =>
                x.Name.ToLower() == name.ToLower());
        }

        public List<Game> SearchGames(string keyword)
        {
            return Games
                .Where(x => x.Name.ToLower().Contains(keyword.ToLower())
                         || x.Category.ToLower().Contains(keyword.ToLower()))
                .ToList();
        }

        public void ShowGames()
        {
            Console.WriteLine("=== PAID GAMES ===");

            var paidGames = Games.Where(x => x.Price > 0).ToList();

            if (paidGames.Count == 0)
            {
                Console.WriteLine("No paid games.");
            }
            else
            {
                foreach (var game in paidGames)
                {
                    game.ShowInfo();
                }
            }

            Console.WriteLine("\n=== FREE GAMES ===");

            var freeGames = Games.Where(x => x.Price == 0).ToList();

            if (freeGames.Count == 0)
            {
                Console.WriteLine("No free games.");
            }
            else
            {
                foreach (var game in freeGames)
                {
                    game.ShowInfo();
                }
            }
        }

        private void Save()
        {
            JsonService.Save(PATH, Games);
        }
    }
}