using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackCore;
using TwoTrackExtensions;
using TwoTrackUseCaseScenarioTests.FakesAndMocks;
using TwoTrackUseCaseScenarioTests.PersistenceModel;
using Xunit;

namespace TwoTrackUseCaseScenarioTests
{
    /// <summary>
    /// These tests demonstrate the difference between code using TwoTrack and equivalent code that doesn't.
    /// </summary>
    public class GetOrders
    {
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;
        private readonly Logger _logger;

        public GetOrders()
        {
            _logger = new Logger();
            var context = new FakeShopContext();
            _userRepository = new UserRepository(context);
            _orderRepository = new OrderRepository(context);
        }

        [Fact]
        public void GetOrdersByUserName_NaiveImplementation()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            var user = _userRepository.GetByUserName(userName); // step 3 (act)
            var orders = _orderRepository.GetOrders(user); // step 4 (act)
            var toReturn = orders;
            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }

        [Fact]
        public void GetOrdersByUserName_WithoutUsingTwoTrack()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            User user = default; // step 1 (arrange):
            ICollection<Order> orders = default; // step 1 (arrange):
            try
            {
                user = _userRepository.GetByUserName(userName); // step 3 (act)
                if (user is null) _logger.Log(TwoTrackError.ResultNullError()); // step 5 (logging)
                else orders = _orderRepository.GetOrders(user); // step 4 (act)
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase) // step 2 (arrange)
            {
                _logger.Log(TwoTrackError.Exception(e)); // step 5 (logging)
            }
            orders ??= new List<Order>(); // step 6 (final null handling)
            var toReturn = orders;

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }

        [Fact]
        public void GetOrdersByUserNameWithTwoTrack()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act

            // step 1 (arrange)
            var (user, orders) = TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase)
                .Enclose(() => userName)
            // step 2 (act)
                .Select(_userRepository.GetByUserName) // step 3 (db call)
                .Enclose(_orderRepository.GetOrders) // step 4 (db call)

            // step 3 (log potential errors and return values
                .LogErrors(_logger.Log) // step 5 (logging)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final null handling)
            var toReturn = orders;

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }
    }

}
