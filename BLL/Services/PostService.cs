using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class PostService: IPostService
    {
        private IMapper _mapper;
        IUnitOfWork _uow;

        public PostService(IUnitOfWork uow, 
                           IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<PostDTO> GetAll()
        {
            IEnumerable<Post> posts = _uow.Posts.GetAll();
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return postsDTO;
        }

        public PostDTO Get(int id)
        {
            Post post = _uow.Posts.Get(id);
            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }

        public IEnumerable<PostDTO> GetByAuthor(UserDTO user)
        {
            User dbuser = _mapper.Map<User>(user);
            IEnumerable<Post> posts = _uow.Posts.GetByAuthor(dbuser);
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return postsDTO;
        }

        public PostDTO Add(PostDTO post)
        {
            Post dbpost = _mapper.Map<Post>(post);
            dbpost.CreationDate = DateTime.Now;
            _uow.Posts.Add(dbpost);
            _uow.Save();
            return _mapper.Map<PostDTO>(dbpost);
        }

        public void Delete(int id)
        {
            _uow.Posts.Delete(id);
            _uow.Save();
        }

        public void Update(PostDTO post)
        {
            Post dbpost = _mapper.Map<Post>(post);
            _uow.Posts.Update(dbpost);
            _uow.Save();
        }
    }
}
