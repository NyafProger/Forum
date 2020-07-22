using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
    }
}
