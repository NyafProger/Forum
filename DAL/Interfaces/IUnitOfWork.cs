using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
