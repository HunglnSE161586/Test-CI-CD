using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private eBookStoreContext _context;
        public PublisherRepository() : base(new eBookStoreContext())
        {
            _context = new eBookStoreContext();
        }
    }
}
