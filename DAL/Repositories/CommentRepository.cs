using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        ForumContext _db;

        public CommentRepository(ForumContext db) : base(db)
        {
            _db = db;
        }

        public override Comment Get(int id)
        {
            return _db.Comments.Where(comm => comm.Id == id)
                .Include(comm => comm.Author)
                .Include(comm => comm.Post)
                .FirstOrDefault();
        }

        public override IEnumerable<Comment> GetAll()
        {
            return _db.Comments
                .Include(comm => comm.Author)
                .Include(comm => comm.Post);
        }

        public IEnumerable<Comment> GetByPost(int postId)
        {
            return _db.Comments.Where(comm => comm.Post.Id == postId)
                .Include(comm => comm.Author)
                .Include(comm => comm.Post);
        }
    }
}
