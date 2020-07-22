using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
        public IEnumerable<Post> GetByAuthor(User user);
    }
}
