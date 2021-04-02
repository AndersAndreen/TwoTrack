using System;

namespace TwoTrackUseCaseScenarioTests.PersistenceModel
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }

        public Product(int productId, string name, string unit)
        {
            ProductId = productId;
            Name = name;
            Price = (decimal)(DateTime.Now.Second / 2.0);
            Unit = unit;
        }
    }
}
