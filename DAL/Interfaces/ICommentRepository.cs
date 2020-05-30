using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        public IEnumerable<Comment> GetByPost(Post post);
    }
}
