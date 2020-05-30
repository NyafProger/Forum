using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        public IEnumerable<CommentDTO> GetByPost(PostDTO post);
        public IEnumerable<CommentDTO> GetAll();
        public CommentDTO Get(int id);
        public CommentDTO Add(CommentDTO comment);
        public void Delete(int id);
        public void Update(CommentDTO comment);
    }
}
