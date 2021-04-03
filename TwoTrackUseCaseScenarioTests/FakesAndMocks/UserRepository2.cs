using System.Linq;
using TwoTrackCore;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class UserRepository2
    {
        private readonly FakeShopContext _context;

        public UserRepository2(FakeShopContext context)
        {
            _context = context;
        }

        public ITwoTrack<User> GetByUserId(int userId) =>
            TwoTrack.Enclose(() => _context.Users.FirstOrDefault(user => user.UserId == userId));

        public ITwoTrack<User> GetByUserName(string userName) =>
            TwoTrack.Enclose(() => _context.Users.FirstOrDefault(user => user.UserName == userName));
    }
}
