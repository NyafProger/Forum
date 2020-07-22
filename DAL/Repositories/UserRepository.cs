using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
                .FirstOrDefault();
        }

        public override IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetByName(string Name)
        {
            return _db.Users.Where(user => user.UserName == Name)
                .FirstOrDefault();
        }

        public User GetByEmail(string Email)
        {
            return _db.Users.Where(user => user.Email == Email)
                .FirstOrDefault();
        }
    }
}
