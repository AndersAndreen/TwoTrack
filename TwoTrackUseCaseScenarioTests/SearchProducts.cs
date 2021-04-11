using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TwoTrackCore;
using TwoTrackExtensions;
using TwoTrackUseCaseScenarioTests.FakesAndMocks;
using TwoTrackUseCaseScenarioTests.PersistenceModel;
using Xunit;

namespace TwoTrackUseCaseScenarioTests
{
    public class SearchProducts
    {
        private readonly FakeShopContext _context;
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;

        public SearchProducts()
        {
            _context = new FakeShopContext();
            _userRepository = new UserRepository(_context);
            _orderRepository = new OrderRepository(_context);
        }

        [Fact]
        public void SearchProductsWithNameContainingString()
        {
            // Arrange
            var searchString = "red";

            // Act
            var result1 = TwoTrack.Ok()
                .Enclose(() => _context.Products.Where(product => product.Name.Contains(searchString)).ToList());

            // Assert
            result1.Succeeded.Should().BeTrue();
            result1.Do(products => products.Count().Should().Be(3));
        }

    }
}
