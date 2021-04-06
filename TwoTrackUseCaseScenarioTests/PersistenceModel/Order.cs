using System.Collections.Generic;
using System.Linq;

namespace TwoTrackUseCaseScenarioTests.PersistenceModel
{
    internal class Order
    {
        public int OrderId { get; private set; }
        public int UserId { get; private set; }
        public ICollection<LineItem> LineItems { get; private set; }

        public Order(int orderId, int userId, IEnumerable<LineItem> lineItems)
        {
            OrderId = orderId;
            UserId = userId;
            LineItems = lineItems.ToList();
        }
    }
}
