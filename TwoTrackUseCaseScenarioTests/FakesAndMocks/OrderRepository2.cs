using System.Collections.Generic;
using System.Linq;
using TwoTrackCore;
using TwoTrackUseCaseScenarioTests.PersistenceModel;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class OrderRepository2
    {
        private readonly FakeShopContext _context;

        public OrderRepository2(FakeShopContext context)
        {
            _context = context;
        }

        public bool IsSaved { get; private set; }

        public ITwoTrack<ICollection<Order>> GetOrders(User user) =>
            TwoTrack.Enclose(() => _context.Orders.Where(order => order.UserId == user.UserId).ToList());

        internal ITwoTrack<Order> PlaceOrder(Order order) =>
            TwoTrack.Enclose(() =>
            {
                var nextId = _context.Orders.Max(o => o.OrderId) + 1;
                var newOrder = new Order(nextId, order.UserId, order.LineItems);
                _context.Orders.Add(newOrder);
                IsSaved = false;
                return newOrder;
            });

        internal ITwoTrack SaveChanges() =>
            TwoTrack.Enclose(() =>
            {
                IsSaved = true;
                return TwoTrack.Ok();
            });
    }
}

