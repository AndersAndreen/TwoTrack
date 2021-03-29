using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.EntityModels
{
    public class FakeDbContext
    {
        public IQueryable<Book> Books =>
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

        //private IQueryable<OrderViewModel> FakeRepoGetOrders()
        //    => new List<OrderViewModel>
        //    {
        //        new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
        //        new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
        //        new OrderViewModel{ID = 1, Amonunt = 1, TotalPrice = 1.2m},
        //    }.AsQueryable();
    }
}
