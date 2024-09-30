namespace CallorieCounter
{
    using CallorieCounter.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private static List<User> users = new List<User>
    {
        new User { Id = 1, Name = "Тимур", Age = 21, Weight = 70, Height = 175 },
        new User { Id = 2, Name = "Алиса", Age = 25, Weight = 60, Height = 165 }
    };

        public IEnumerable<User> GetUsers()
        {
            return users.ToList();
        }

        public User GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public User CreateUser(User newUser)
        {
            newUser.Id = users.Count + 1;
            users.Add(newUser);
            return newUser;
        }

        public void UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Age = updatedUser.Age;
                user.Weight = updatedUser.Weight;
                user.Height = updatedUser.Height;
            }
        }

        public void DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
        }
    }
}
