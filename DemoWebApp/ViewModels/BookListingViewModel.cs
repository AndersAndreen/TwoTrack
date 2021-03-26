using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.ViewModels
{
    public class BookListingViewModel
    {
        public string Searchfor { get; set; }
        public ICollection<BookViewModel> Books = new List<BookViewModel>();
    }
}
