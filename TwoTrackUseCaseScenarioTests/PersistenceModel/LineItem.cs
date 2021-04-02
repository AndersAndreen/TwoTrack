using System;

namespace TwoTrackUseCaseScenarioTests.PersistenceModel
{
    internal class LineItem
    {
        public int LineItemId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public LineItem(int lineItemId, int productId, int amount)
        {
            LineItemId = lineItemId;
            ProductId = productId;
            Amount = amount;
        }
    }
}
