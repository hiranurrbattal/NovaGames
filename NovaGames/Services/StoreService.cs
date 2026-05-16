using NovaGames.Managers;
using NovaGames.Models;
using NovaGames.Exceptions;

namespace NovaGames.Services
{
    public class StoreService
    {
        private GameStore store;
        private AchievementManager achievement = new AchievementManager();

        public StoreService(GameStore gameStore)
        {
            store = gameStore;

            // başlangıç oyunları
            if (store.Games.Count == 0)
            {
                store.AddGame(new PaidGame
                {
                    Name = "Cyber Quest",
                    Category = "RPG",
                    Price = 150
                });

                store.AddGame(new PaidGame
                {
                    Name = "Space Wars",
                    Category = "Action",
                    Price = 200
                });

                store.AddGame(new FreeGame
                {
                    Name = "Pixel Runner",
                    Category = "Arcade"
                });
            }
        }

        public void OpenStore(User user)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== STORE MENU ===");
                Console.WriteLine("1 - View Games / Buy Game");
                Console.WriteLine("2 - Search Game");
                Console.WriteLine("3 - Exit");
                Console.Write("Select: ");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        BuyGame(user);
                        break;

                    case "2":
                        SearchGame(user);
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void BuyGame(User user)
        {
            Console.Clear();

            store.ShowGames();

            Console.WriteLine("\nBalance: " + user.Balance);
            Console.WriteLine("\nEnter game name to buy (X to exit): ");

            string input = Console.ReadLine();

            if (input.ToLower() == "x")
                return;

            TryBuyGame(user, input);
        }

        private void SearchGame(User user)
        {
            Console.Clear();

            Console.Write("Search: ");
            string keyword = Console.ReadLine();

            var results = store.SearchGames(keyword);

            Console.WriteLine("\n=== RESULTS ===");

            if (results.Count == 0)
            {
                Console.WriteLine("No games found.");
                Console.ReadKey();
                return;
            }

            foreach (var game in results)
            {
                game.ShowInfo();
            }

            Console.WriteLine("\nEnter game name to buy (X to exit): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "x")
                return;

            TryBuyGame(user, input);
        }

        private void TryBuyGame(User user, string input)
        {
            try
            {
                var game = store.GetGame(input);

                if (game == null)
                    throw new GameNotFoundException();

                if (user.Library.Contains(game.Name))
                    throw new DuplicatePurchaseException();

                if (user.Balance < game.Price)
                    throw new InsufficientBalanceException();

                user.Balance -= game.Price;
                user.Library.Add(game.Name);

                UserManager userManager = new UserManager();

                achievement.CheckAchievements(user, userManager);

                userManager.UpdateUser(user);

                Console.WriteLine("Purchased: " + game.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}