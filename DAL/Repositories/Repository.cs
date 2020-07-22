using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ForumContext _db;

        public Repository(ForumContext db)
        {
            _db = db;
        }

        public virtual void Add(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            T item = _db.Set<T>().Find(id);
            if (item != null)
                _db.Set<T>().Remove(item);
        }

        public virtual T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public void Update(T item)
        {
            _db.Set<T>().Update(item);
        }
    }
}
