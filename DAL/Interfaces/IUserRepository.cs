using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        public User GetByName(string Name);
        public User GetByEmail(string Email);
    }
}
