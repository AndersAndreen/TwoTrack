using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class OrderRepository
    {
        private readonly FakeShopContext _context = new FakeShopContext();

        public ICollection<Order> GetOrders(User user) =>
            _context.Orders.Where(order => order.UserId == user.UserId).ToList();
    }
}
