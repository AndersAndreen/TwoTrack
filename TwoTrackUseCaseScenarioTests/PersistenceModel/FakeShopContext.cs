using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TwoTrackUseCaseScenarioTests.PersistenceModel
{
    internal class FakeShopContext
    {
        public IEnumerable<User> Users =>
            new List<User>
            {
                new User(1, "ClarkKent"),
                new User(4, "LouisLane"),
                new User(5, "LexLuthor"),
                new User(6, "Mxyzptlk"),
            };

        public IEnumerable<Product> Products =>
            new List<Product>
            {
                // pc = piece, up = unit package, m = meters, kg = kilo gram
                new Product(1,"horn-rimmed glasses", "pc"),
                new Product(2, "striped suit", "pc"),
                new Product(3, "black suit", "pc"),
                new Product(4, "Pencil", "up"),
                new Product(5, "blue notebook", "pc"),
                new Product(6, "plue spandex", "m"),
                new Product(7, "red spandex", "m"),
                new Product(7, "bowler hat", "pc"),
                new Product(10, "Kryptonite, green", "kg"),
                new Product(11, "Kryptonite, red", "kg"),
                new Product(12, "Kryptonite, gold", "kg"),
                new Product(13, "red notebook", "pc"),
            };


        public IEnumerable<Order> Orders =>
            new List<Order>
            {
                new Order(100,1,LineItems.Where(item=>new []{1,2,4,5}.Contains(item.LineItemId)).ToList()),
                new Order(101,1,LineItems.Where(item=>new []{8,9}.Contains(item.LineItemId)).ToList()),
                new Order(102,4,LineItems.Where(item=>new []{10,11}.Contains(item.LineItemId)).ToList()),
                new Order(103,5,LineItems.Where(item=>new []{4,5}.Contains(item.LineItemId)).ToList()),
                new Order(104,5,LineItems.Where(item=>new []{6,7}.Contains(item.LineItemId)).ToList()),
                new Order(105,6,LineItems.Where(item=>new []{12}.Contains(item.LineItemId)).ToList()),
            };

        public IEnumerable<LineItem> LineItems =>
            new List<LineItem>
            {
                new LineItem(1,1,1),
                new LineItem(2,6,5),
                new LineItem(3,7,4),
                new LineItem(4,3,4),
                new LineItem(5,10,100),
                new LineItem(6,11,15),
                new LineItem(7,12,7),
                new LineItem(8,6,5),
                new LineItem(9,7,4),
                new LineItem(10,4,3),
                new LineItem(11,5,7),
                new LineItem(12,7,1),
            };
    }
}
