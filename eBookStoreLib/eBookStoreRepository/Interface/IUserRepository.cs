using eBookStoreLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository.Interface
{
    public interface IUserRepository:IRepository<User>
    {
        User Get(string email, string password);
    }
}
