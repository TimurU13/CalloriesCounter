using CallorieCounter.Models;

namespace CallorieCounter
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User newUser);
        void UpdateUser(int id, User updatedUser);
        void DeleteUser(int id);
    }
}
