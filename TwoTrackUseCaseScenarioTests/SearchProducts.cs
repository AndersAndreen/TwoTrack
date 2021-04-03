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
        private readonly FakeShopContext _context = new FakeShopContext();
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly OrderRepository _orderRepository = new OrderRepository();

        [Fact]
        public void SearchProductsWithNameContainingString()
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

    }
}
