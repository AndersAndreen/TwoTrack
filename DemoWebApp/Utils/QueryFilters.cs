using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoWebApp.EntityModels;
using DemoWebApp.ViewModels;

namespace DemoWebApp.Utils
{
    public class QueryFilters
    {
        public static Expression<Func<Book, bool>> FindInTitleAndAuthor(string searchfor)
        {
            if (searchfor is null) return book => false;
            return book => book.Author.ToLower().Contains(searchfor) || book.Title.ToLower().Contains(searchfor);
        }
    }
}
