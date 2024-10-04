using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T GetByID(object id);
        //void Delete(object id);
        void Delete(T entityToDelete);
        void Insert(T entity);
        void Update(T entityToUpdate);
    }

}
