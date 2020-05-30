using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        ForumContext _db;

        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
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
        public IPostRepository Posts { get; }
        public ICommentRepository Comments { get; }
        public IUserRepository Users { get; }

        private bool disposed = false;

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
