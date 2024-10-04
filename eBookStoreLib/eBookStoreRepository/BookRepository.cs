using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private eBookStoreContext _context;
        public BookRepository() : base(new eBookStoreContext())
        {
            _context = new eBookStoreContext();
        }
    }
}
