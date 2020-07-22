using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        public IEnumerable<Comment> GetByPost(int postId);
    }
}
