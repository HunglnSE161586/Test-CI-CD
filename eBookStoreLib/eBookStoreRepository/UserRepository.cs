using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private eBookStoreContext context;
        public UserRepository() : base(new eBookStoreContext())
        {
            context = new eBookStoreContext();
        }

        public User Get(string email, string password)
        {
            User result;
            try
            {
                result = context.Users.FirstOrDefault(u => u.EmailAddress.Equals(email)
                && u.Password.Equals(password));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
