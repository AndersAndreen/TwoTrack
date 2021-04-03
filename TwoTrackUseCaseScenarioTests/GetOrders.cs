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
        private readonly Logger _logger = new Logger();

        public GetOrders()
        {
            var context = new FakeShopContext();
            _userRepository = new UserRepository(context);
            _orderRepository = new OrderRepository(context);
        }

        [Fact]
        public void GetOrdersByUserNameWithTwoTrack()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            var (user, orders) = TwoTrack.Enclose(() => userName) // step 1 (arrange)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase) // step 2 (arrange)
                .Select(_userRepository.GetByUserName) // step 3 (db call)
                .Enclose(_orderRepository.GetOrders) // step 4 (db call)
                .LogErrors(_logger.Log) // step 5 (logging)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final null handling)

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
                if (user is null) _logger.Log(TtError.ResultNullError()); // step 5 (logging)
                else orders = _orderRepository.GetOrders(user); // step 4 (act)
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase) // step 2 (arrange)
            {
                _logger.Log(TtError.Exception(e)); // step 5 (logging)
            }
            user ??= User.Empty(); // step 6 (final null handling)
            orders ??= new List<Order>(); // step 6 (final null handling)

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }


    }

}
