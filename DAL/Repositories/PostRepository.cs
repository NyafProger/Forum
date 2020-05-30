using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class PostRepository: Repository<Post>, IPostRepository
    {
        ForumContext _db;
        public PostRepository(ForumContext db) : base(db)
        {
            _db = db;
        }

        public override Post Get(int id)
        {
            return _db.Posts.Where(post => post.Id == id)
                .Include(post => post.Author)
                .Include(post => post.Comments)
                .FirstOrDefault();
        }
        public override IEnumerable<Post> GetAll()
        {
            return _db.Posts
                .Include(post => post.Author)
                .Include(post => post.Comments);
        }
        public IEnumerable<Post> GetByAuthor(User user)
        {
            return _db.Posts.Where(post => post.Author == user)
                .Include(post => post.Author)
                .Include(post => post.Comments);
        }
        public override void Add(Post item)
        {
            item.CreationDate = DateTime.Now;
            base.Add(item);
        }
    }
}
