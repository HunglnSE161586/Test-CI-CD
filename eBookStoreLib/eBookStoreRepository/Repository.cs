using eBookStoreLib.DataAccess;
using eBookStoreLib.eBookStoreRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreLib.eBookStoreRepository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private eBookStoreContext context;
        private DbSet<T> dbSet;
        public Repository(eBookStoreContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> Get()
        {
            return dbSet.AsQueryable();
        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        //public void Delete(object id)
        //{
        //    T entityToDelete = dbSet.Find(id);
        //    Delete(entityToDelete);
        //    context.SaveChanges();
        //}

        public void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
