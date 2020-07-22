using DAL.Interfaces;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        ForumContext _db;
        public IPostRepository Posts { get; }
        public ICommentRepository Comments { get; }
        public IUserRepository Users { get; }
        private bool disposed = false;

        public UnitOfWork(ForumContext db,
                          IPostRepository posts,
                          ICommentRepository comments,
                          IUserRepository users)
        {
            _db = db;
            Posts = posts;
            Users = users;
            Comments = comments;
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
