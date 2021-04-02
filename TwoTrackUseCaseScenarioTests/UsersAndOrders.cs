using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using TwoTrackCore;
using TwoTrackExtensions;
using TwoTrackUseCaseScenarioTests.PersistenceModel;
using Xunit;

namespace TwoTrackUseCaseScenarioTests
{
    public class UsersAndOrders
    {
        private readonly FakeShopContext _context;

        public UsersAndOrders()
        {
            _context = new FakeShopContext();
        }
        [Fact]
        public void SearchProducts()
        {
            // Arrange
            var searchString = "red";

            // Act
            var result1 = TwoTrack
                .Enclose(() => _context.Products.Where(product => product.Name.Contains(searchString)).ToList());

            // Assert
            result1.Succeeded.Should().BeTrue();
            result1.Do(products => products.Count().Should().Be(3));
        }

        [Fact]
        public void GetOrdersByUserName()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            var (user, orders) = TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase)
                .Enclose(() => _context.Users.FirstOrDefault(user1 => user1.UserName == userName))
                .Enclose(user1 => _context.Orders.Where(order => order.UserId == user1.UserId).ToList())
                .ValueOrDefault((User.Empty(), new List<Order>()));

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }
    }

    public class SomeExceptionThownByDatabase : Exception
    {
        public SomeExceptionThownByDatabase()
        {
        }

        public SomeExceptionThownByDatabase(string message)
            : base(message)
        {
        }

        public SomeExceptionThownByDatabase(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
