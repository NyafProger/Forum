using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostDTO> GetAll();
        public PostDTO Get(int id);
        public IEnumerable<PostDTO> GetByAuthor(UserDTO user);
        public PostDTO Add(PostDTO post);
        public void Delete(int id);
        public void Update(PostDTO post);
    }
}
