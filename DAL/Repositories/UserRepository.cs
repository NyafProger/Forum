using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        ForumContext _db;
        public UserRepository(ForumContext db) : base(db)
        {
            _db = db;
        }
        public override User Get(int id)
        {
            return _db.Users.Where(user => user.Id == id)
                .Include(user => user.Posts)
                .FirstOrDefault();
        }
        public override IEnumerable<User> GetAll()
        {
            return _db.Users
                .Include(user => user.Posts);
        }
        public User GetByName(string Name)
        {
            return _db.Users.Where(user => user.UserName == Name)
                .Include(user => user.Posts)
                .FirstOrDefault();
        }
        public User GetByEmail(string Email)
        {
            return _db.Users.Where(user => user.Email == Email)
                .Include(user => user.Posts)
                .FirstOrDefault();
        }
    }
}
