using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
