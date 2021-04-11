using FluentAssertions;
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
    /// These tests demonstrate that Select and Enclose can handle return values of type ITwoTrack<T> as well as of type T.
    /// </summary>
    public class GetOrders2
    {
        private readonly UserRepository2 _userRepository;
        private readonly OrderRepository2 _orderRepository;
        private readonly Logger _logger;

        public GetOrders2()
        {
            _logger = new Logger();
            var context = new FakeShopContext();
            _userRepository = new UserRepository2(context);
            _orderRepository = new OrderRepository2(context);
        }

        [Fact]
        public void GetOrdersByUserNameUsigRepositories()
        {
            // Arrange
            var userName = "ClarkKent";

            // Act
            var (user, orders) = TwoTrack.Ok().Enclose(() => userName) // step 1 (arrange)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase) // step 2 (arrange)
                .Select(_userRepository.GetByUserName) // step 3 (db call)
                .Enclose(_orderRepository.GetOrders) // step 4 (db call)
                .LogErrors(_logger.Log) // step 5 (logging)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final null handling)

            // Assert
            user.UserId.Should().Be(1);
            orders.Count().Should().Be(2);
        }
    }
}
