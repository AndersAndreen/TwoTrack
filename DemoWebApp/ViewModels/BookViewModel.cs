using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DemoWebApp.EntityModels;

namespace DemoWebApp.ViewModels
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string Isbn { get; set; }
        public decimal PriceInSek { get; set; }

        public static Expression<Func<Book, BookViewModel>> Map() =>
            book => new BookViewModel
            {
                Isbn = book.Isbn,
                Author = book.Author,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                PriceInSek = book.PriceInSek
            };
    }
}
