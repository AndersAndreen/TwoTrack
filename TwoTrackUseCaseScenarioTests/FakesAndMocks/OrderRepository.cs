using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class OrderRepository
    {
        private readonly FakeShopContext _context;

        public OrderRepository(FakeShopContext context)
        {
            _context = context;
        }
        public bool IsSaved { get; private set; }

        public ICollection<Order> GetOrders(User user) =>
            _context.Orders.Where(order => order.UserId == user.UserId).ToList();

        internal int PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            return _context.Orders.Count;
        }

        internal void SaveChanges()
        {
            IsSaved = true;
        }
    }
}
