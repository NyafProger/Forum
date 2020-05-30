using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T Get(int id);
        public void Add(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
