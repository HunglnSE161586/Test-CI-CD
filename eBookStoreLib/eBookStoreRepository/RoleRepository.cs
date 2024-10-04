using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private eBookStoreContext _context;
        public RoleRepository() : base(new eBookStoreContext())
        {
            _context = new eBookStoreContext();
        }
    }
}
