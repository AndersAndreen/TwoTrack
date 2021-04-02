using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
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
            var (user, orders) = TwoTrack.Ok() // step 1 (arrange)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase) // step 2 (arrange)
                .Enclose(() => _context.Users.FirstOrDefault(user1 => user1.UserName == userName)) // step 3 (act)
                .Enclose(user1 => _context.Orders.Where(order => order.UserId == user1.UserId).ToList()) // step 4 (act)
                .LogErrors(errors => Log(errors.ToArray())) // step 5 (logging)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final nullcheck)

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }

        [Fact]
        public void GetOrdersByUserName_WithoutTwoTrack()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            User user = default; // step 1 (arrange):
            List<Order> orders = default; // step 1 (arrange):
            try
            {
                user = _context.Users.FirstOrDefault(user1 => user1.UserName == userName); // step 3 (act)
                if( user is null) Log(TtError.ResultNullError()); // step 5 (logging)
                else orders = _context.Orders.Where(order => order.UserId == user?.UserId).ToList(); // step 4 (act)
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase) // step 2 (arrange)
            {
                Log(TtError.Exception(e)); // step 5 (logging)
            }
            user ??= User.Empty(); // step 6 (nullcheck)
            orders ??= new List<Order>(); // step 6 (final nullcheck)

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }

        private void Log(params TtError[] errors)
        {
            throw new NotImplementedException();
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
