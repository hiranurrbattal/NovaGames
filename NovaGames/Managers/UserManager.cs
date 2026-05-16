using NovaGames.Models;
using NovaGames.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaGames.Managers
{
    public class UserManager
    {
        private const string PATH = "users.json";

        public List<User> Users { get; set; }

        public UserManager()
        {
            Users = JsonService.Load<List<User>>(PATH);

            if (Users == null)
                Users = new List<User>();

            // 🔥 DEFAULT ADMIN (DOĞRU YER)
            if (!Users.Any(x => x.Username == "admin"))
            {
                Users.Add(new User
                {
                    Username = "admin",
                    Password = "123",
                    Balance = 999999,
                    Library = new List<string>(),
                    Achievements = new List<string>()
                });

                Save();
            }
        }

        public void Register(string username, string password)
        {
            if (Users.Any(x => x.Username == username))
            {
                Console.WriteLine("User already exists!");
                return;
            }

            Users.Add(new User
            {
                Username = username,
                Password = password,
                Balance = 1000,
                Library = new List<string>(),
                Achievements = new List<string>()
            });

            Save();
        
        }
        public User Login(string username, string password)
        {
            return Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);
        }

        public void UpdateUser(User user)
        {
            var old = Users.FirstOrDefault(x => x.Username == user.Username);

            if (old != null)
            {
                old.Balance = user.Balance;
                old.Library = user.Library ?? new List<string>();
                old.Achievements = user.Achievements ?? new List<string>();
            }

            Save();
        }

        private void Save()
        {
            JsonService.Save(PATH, Users);
        }
    }
}