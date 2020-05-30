using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
        public IEnumerable<Post> GetByAuthor(User user);
    }
}
