using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class UserRepository
    {
        private readonly FakeShopContext _context = new FakeShopContext();

        public User GetByUserName(string userName) => _context.Users.FirstOrDefault(user1 => user1.UserName == userName);
    }
}
