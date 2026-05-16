using NovaGames.Services;
using NovaGames.Models;
using NovaGames.Managers;

namespace NovaGames.Menus
{
    public class MainMenu
    {
        private AuthService auth;
        private GameStore store;
        private StoreService storeService;
        private AdminService adminService;

        public MainMenu()
        {
            store = new GameStore();
            storeService = new StoreService(store);
            adminService = new AdminService(store);
            auth = new AuthService();
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("         NOVAGAMES");
                Console.WriteLine("================================");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Register");
                Console.WriteLine("3 - Exit");
                Console.Write("Select: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        User user = auth.Login();

                        if (user == null)
                        {
                            Console.WriteLine("Login failed!");
                            Console.ReadKey();
                            break;
                        }

                        if (user.Username.ToLower() == "admin")
                            adminService.OpenAdminPanel();
                        else
                            UserMenu(user);

                        break;

                    case "2":
                        auth.Register();
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

        private void UserMenu(User user)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== USER MENU ===");
                Console.WriteLine("1 - Store");
                Console.WriteLine("2 - Profile");
                Console.WriteLine("3 - Add Balance");
                Console.WriteLine("4 - Rate Game");
                Console.WriteLine("5 - Logout");
                Console.Write("Select: ");

                string c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        storeService.OpenStore(user);
                        break;

                    case "2":
                        user.ShowProfile();
                        Console.ReadKey();
                        break;

                    case "3":
                        AddBalance(user);
                        break;

                    case "4":
                        RateGame(user);
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

        private void AddBalance(User user)
        {
            try
            {
                Console.Write("Enter amount to add: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                user.AddBalance(amount);

                UserManager userManager = new UserManager();
                userManager.UpdateUser(user);

                Console.WriteLine("Balance added successfully!");
                Console.WriteLine("New Balance: " + user.Balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }

        private void RateGame(User user)
        {
            Console.Clear();

            if (user.Library == null || user.Library.Count == 0)
            {
                Console.WriteLine("You don't own any games yet.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("=== YOUR LIBRARY ===");

            foreach (var game in user.Library)
            {
                Console.WriteLine("- " + game);
            }

            Console.Write("\nEnter game name to rate: ");
            string gameName = Console.ReadLine();

            if (!user.Library.Contains(gameName))
            {
                Console.WriteLine("You can only rate games in your library!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Enter rating (1-10): ");
                double rating = Convert.ToDouble(Console.ReadLine());

                if (rating < 1 || rating > 10)
                {
                    Console.WriteLine("Rating must be between 1 and 10.");
                    Console.ReadKey();
                    return;
                }

                store.RateGame(gameName, rating);

                Console.WriteLine("Game rated successfully!");
            }
            catch
            {
                Console.WriteLine("Invalid rating!");
            }

            Console.ReadKey();
        }
    }
}