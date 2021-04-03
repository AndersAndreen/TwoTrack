using System.Linq;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class UserRepository
    {
        private readonly FakeShopContext _context;

        public UserRepository(FakeShopContext context)
        {
            _context = context;
        }

        public User GetByUserId(int userId) => _context.Users.FirstOrDefault(user => user.UserId == userId);
        public User GetByUserName(string userName) => _context.Users.FirstOrDefault(user => user.UserName == userName);
    }
}
