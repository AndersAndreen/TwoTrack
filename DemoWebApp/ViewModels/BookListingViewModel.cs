using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.ViewModels
{
    public class BookListingViewModel
    {
        public ICollection<BookViewModel> Books = new List<BookViewModel>();
    }
}
