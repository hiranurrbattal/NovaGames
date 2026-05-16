using NovaGames.Models;

namespace NovaGames.Managers
{
    public class AchievementManager
    {
        public void CheckAchievements(User user, UserManager userManager)
        {
            if (user.Achievements == null)
                user.Achievements = new List<string>();

            if (user.Library == null)
                user.Library = new List<string>();

            AddAchievement(user, "Welcome Player", userManager);

            if (user.Library.Count >= 1)
                AddAchievement(user, "First Purchase", userManager);

            if (user.Library.Count >= 3)
                AddAchievement(user, "Collector", userManager);

            if (user.Balance <= 500)
                AddAchievement(user, "Big Spender", userManager);
        }

        private void AddAchievement(User user, string achievement, UserManager userManager)
        {
            if (!user.Achievements.Contains(achievement))
            {
                user.Achievements.Add(achievement);
                userManager.UpdateUser(user);

                Console.WriteLine("🏆 Achievement Unlocked: " + achievement);
            }
        }
    }
}