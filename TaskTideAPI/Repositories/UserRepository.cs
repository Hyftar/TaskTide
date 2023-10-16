using TaskTideLib.Models;

namespace TaskTideAPI.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUsername(string  username);
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

        public User GetById(int id)
        {
            var password = UserHelpers.HashPassword("admin123");
            return new User() { Id = 15, Username = "Test", HashedPassword = password };
        }

        public User GetByUsername(string username)
        {

            var password = UserHelpers.HashPassword("admin123");
            return new User() { Id = 15, Username = "Test", HashedPassword = password };
        }
    }
}
