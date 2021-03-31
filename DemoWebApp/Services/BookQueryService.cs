using DemoWebApp.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using DemoWebApp.BusinessRules;
using TwoTrackCore;
using TwoTrackExtensions.TryOut;

namespace DemoWebApp.Services
{
    public class BookQueryService
    {
        private readonly FakeDbContext _fakeDbContext;

        public BookQueryService(FakeDbContext fakeDbContext)
        {
            _fakeDbContext = fakeDbContext;
        }

        public ITwoTrack<ICollection<T>> Get<T>(Expression<Func<Book, bool>> filter, Expression<Func<Book, T>> mapper)
        {
            return TwoTrackCore.TwoTrack.Enclose(() => _fakeDbContext.Books
                .Where(filter)
                .Select(mapper)
                .ToList());
        }

        public ITwoTrack<T> GetByIsbn<T>(string isbn, Expression<Func<Book, T>> mapper)
        {
            var result = TwoTrackCore.TwoTrack.Enclose(() => isbn, IsbnValidator.Validate, TtError.ValidationError($"incorrect ISBN format: {isbn}"))
                .Enclose(nr => _fakeDbContext.Books
                    .Where(book => book.Isbn == nr)
                    .Select(mapper)
                    .FirstOrDefault())
                .Select(tuple => tuple.Item2);
                //.ReplaceNullResultWithErrorMessage(ErrorDescriptions.ItemNotFound);
            return result;
        }

        public ITwoTrack<T> GetByIsbn2<T>(string isbn, Expression<Func<Book, T>> mapper)
        {
            //var x = TwoTrack.Enclose(() => "#").Enclose(xx => 34);
            var result = TwoTrackCore.TwoTrack.Ok()
                //.ValidateAlways(() => IsbnValidator.Validate(isbn + "#"), $"incorrect ISBN format")
                .Enclose(() => _fakeDbContext.Books
                    .Where(book => book.Isbn == isbn)
                    .Select(mapper)
                    .FirstOrDefault());
                //.ReplaceNullResultWithErrorMessage(ErrorDescriptions.ItemNotFound);
            return result;
        }
    }
}

