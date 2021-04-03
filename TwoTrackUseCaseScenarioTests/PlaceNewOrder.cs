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
    public class PlaceNewOrder
    {
        private readonly FakeShopContext _context;
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;

        private readonly Logger _logger = new Logger();

        public PlaceNewOrder()
        {
            _context = new FakeShopContext();
            _userRepository = new UserRepository(_context);
            _orderRepository = new OrderRepository(_context);
        }

        [Fact]
        public void PlaceNewOrderThenGetOrders()
        {
            // Arrange
            var userName = "LouisLane";
            var lineItems = new List<LineItem>
            {
                new LineItem(100, 13, 1), // red notebook
                new LineItem(100, 6, 3), // blue spandex
                new LineItem(100, 7, 4), // red spandex
            };

            // Act
            var resultSave = TwoTrack.Enclose(() => userName)
                .Select(_userRepository.GetByUserName)
                .Select(user =>
                {
                    var order1 = new Order(1, user.UserId, new List<LineItem> { });
                    lineItems.ToList().ForEach(item=> order1.LineItems.Add(item));
                    return order1;
                })
                .Select(order1 => _orderRepository.PlaceOrder(order1))
                .Do(count => _orderRepository.SaveChanges())
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "order", "Order saved"))
                .LogErrors(_logger.Log);

            var savedOrders = TwoTrack.Enclose(() => userName)
                .Select(_userRepository.GetByUserName)
                .Select(_orderRepository.GetOrders)
                .LogErrors(_logger.Log)
                .ValueOrDefault(new List<Order>());

            // Assert
            resultSave.Succeeded.Should().BeTrue();
            resultSave.Confirmations.Single().Description.Should().Be("Order saved");
            _orderRepository.IsSaved.Should().BeTrue();
            resultSave.Do(orderCount => orderCount.Should().Be(7));
            savedOrders.Count().Should().Be(2);
        }




    }

}
