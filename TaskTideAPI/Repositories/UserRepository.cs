using TaskTideAPI.DataContexts;
using TaskTideAPI.Models;

namespace TaskTideAPI.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);

        User? GetByUsername(string  username);

        User CreateUser(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly TaskTideContext TaskTideContext;
        private readonly ITransactionEventsRepository TransactionEventsRepository;

        public UserRepository(
            TaskTideContext taskTideContext,
            ITransactionEventsRepository transactionEventsRepository)
        {
            this.TaskTideContext = taskTideContext;
            this.TransactionEventsRepository = transactionEventsRepository;
        }

        public User GetById(int id)
        {
            return this.TaskTideContext.Users.Single(x => x.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return this.TaskTideContext.Users.SingleOrDefault(x => x.Username == username);
        }

        public User CreateUser(string username, string password)
        {
            var user =
                new User()
                {
                    Username = username,
                    HashedPassword = UserHelpers.HashPassword(password),
                };

            this.TaskTideContext.Add(user);

            var userDefaultCalendar =
                new Calendar
                {
                    Owner = user,
                    Name = $"{user.Username}'s calendar",
                    IsReadOnly = true,
                };

            this.TaskTideContext.Calendars.Add(userDefaultCalendar);

            this.TransactionEventsRepository.Log(
                user,
                TransactionType.Create,
                user
            );

            this.TransactionEventsRepository.Log(
                userDefaultCalendar,
                TransactionType.Create
            );

            this.TaskTideContext.SaveChanges();

            return user;
        }
    }
}
