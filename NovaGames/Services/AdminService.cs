using NovaGames.Managers;
using NovaGames.Models;

namespace NovaGames.Services
{
    public class AdminService
    {
        private GameStore store;

        public AdminService(GameStore gameStore)
        {
            store = gameStore;
        }

        public void OpenAdminPanel()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ADMIN PANEL ===");
                Console.WriteLine("1 - Add Game");
                Console.WriteLine("2 - Remove Game");
                Console.WriteLine("3 - Update Game");
                Console.WriteLine("4 - View Store");
                Console.WriteLine("5 - Exit");
                Console.Write("Select: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGame();
                        break;

                    case "2":
                        RemoveGame();
                        break;

                    case "3":
                        UpdateGame();
                        break;

                    case "4":
                        store.ShowGames();
                        Console.ReadKey();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddGame()
        {
            try
            {
                Console.Write("Game name: ");
                string name = Console.ReadLine();

                Console.Write("Category: ");
                string category = Console.ReadLine();

                Console.Write("Price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Game game;

                if (price == 0)
                {
                    game = new FreeGame
                    {
                        Name = name,
                        Category = category
                    };
                }
                else
                {
                    game = new PaidGame
                    {
                        Name = name,
                        Category = category,
                        Price = price
                    };
                }

                store.AddGame(game);

                Console.WriteLine("Game added!");
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }

            Console.ReadKey();
        }

        private void RemoveGame()
        {
            Console.Write("Game name: ");
            string name = Console.ReadLine();

            store.RemoveGame(name);

            Console.WriteLine("Game removed!");
            Console.ReadKey();
        }

        private void UpdateGame()
        {
            try
            {
                Console.Write("Enter current game name: ");
                string oldName = Console.ReadLine();

                Console.Write("New game name: ");
                string newName = Console.ReadLine();

                Console.Write("New category: ");
                string newCategory = Console.ReadLine();

                Console.Write("New price: ");
                decimal newPrice = Convert.ToDecimal(Console.ReadLine());

                Game updatedGame;

                if (newPrice == 0)
                {
                    updatedGame = new FreeGame
                    {
                        Name = newName,
                        Category = newCategory
                    };
                }
                else
                {
                    updatedGame = new PaidGame
                    {
                        Name = newName,
                        Category = newCategory,
                        Price = newPrice
                    };
                }

                store.UpdateGame(oldName, updatedGame);

                Console.WriteLine("Game updated!");
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }

            Console.ReadKey();
        }
    }
}