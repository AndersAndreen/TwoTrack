using System;

namespace DemoWebApp.EntityModels
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public decimal PriceInSek { get; set; }

        public static Book Create(string author, string title, int publicationYear, string isbn, decimal priceInSek)
            => new Book
            {
                Id = Guid.NewGuid(),
                Isbn = isbn,
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                PriceInSek = priceInSek
            };
    }
}
