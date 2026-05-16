using NovaGames.Managers;
using NovaGames.Models;

namespace NovaGames.Services
{
    public class AuthService
    {
        private UserManager userManager = new UserManager();

        public void Register()
        {
            Console.Clear();

            Console.WriteLine("=== REGISTER ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                userManager.Register(username, password);
                Console.WriteLine("Register successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public User Login()
        {
            Console.Clear();

            Console.WriteLine("=== LOGIN ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            try
            {
                User user = userManager.Login(username, password);

                if (user == null)
                {
                    Console.WriteLine("Wrong username or password!");
                    Console.ReadKey();
                    return null;
                }

                Console.WriteLine("Login successful!");
                Console.ReadKey();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return null;
            }
        }
    }
}