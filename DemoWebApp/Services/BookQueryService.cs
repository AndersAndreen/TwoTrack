using DemoWebApp.EntityModels;
using DemoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TwoTrackResult;
using TwoTrackResult.Defaults;

namespace DemoWebApp.Services
{
    public class BookQueryService
    {
        public ITwoTrack<ICollection<T>> Get<T>(Expression<Func<Book, bool>> filter, Expression<Func<Book, T>> mapper)
        {
            return TwoTrack.Enclose(() => FakeRepoGetBooks()
                .Where(filter)
                .Select(mapper)
                .ToList());
        }

        public ITwoTrack<T> GetByIsbn<T>(string isbn, Expression<Func<Book, T>> mapper)
            => TwoTrack.Enclose(() => FakeRepoGetBooks()
                .Where(book => book.Isbn == isbn)
                .Select(mapper)
                .FirstOrDefault())
                .ReplaceNullResultWithErrorMessage(ErrorDescriptions.ItemNotFound);

        private IQueryable<Book> FakeRepoGetBooks() =>
            new List<Book>
            {
                Book.Create( "Laura Ingalls Wilder",  "Little House in the Big Woods", 1932,"4412345678",75m),
                Book.Create( "Laura Ingalls Wilder",  "Farmer Boy", 1933,"4412345679",75m),
                Book.Create( "Laura Ingalls Wilder",  "Little House on the Prairie", 1935,"4412345680",59m),
                Book.Create( "Laura Ingalls Wilder",  "On the Banks of Plum Creek", 1937,"4412345681",75m),
                Book.Create( "Jack London",  "The Game", 1905,"451234500234",75m),
                Book.Create( "Jack London",  "The Call of the Wild", 1903,"452234503235",60m),
                Book.Create( "Isaac Asimov",  "The Caves of Steel", 1954,"452234503236",75m),
                Book.Create( "Isaac Asimov",  "The Currents of Space", 1952,"452234503237",102m),
                Book.Create( "Ursula K. Le Guin",  "A Wizard of Earthsea", 1968,"452234500137",120m),
                Book.Create( "Ursula K. Le Guin",  "The Left Hand of Darkness", 1969,"452234500137",115m),
            }.AsQueryable();

        private IQueryable<OrderViewModel> FakeRepoGetOrders()
            => new List<OrderViewModel>
            {
                new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
                new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
                new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
            }.AsQueryable();
    }
}

