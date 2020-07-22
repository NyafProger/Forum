using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CommentService: ICommentService
    {
        private IMapper _mapper;
        IUnitOfWork _uow;

        public CommentService(IUnitOfWork uow, 
                              IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<CommentDTO> GetByPost(int postId)
        {
            IEnumerable<Comment> comments = _uow.Comments.GetByPost(postId);
            var commentsDTO = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            return commentsDTO;
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            IEnumerable<Comment> comments = _uow.Comments.GetAll();
            var commentsDTO = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            return commentsDTO;
        }

        public CommentDTO Get(int id)
        {
            Comment comment = _uow.Comments.Get(id);
            var commentDTO = _mapper.Map<CommentDTO>(comment);
            return commentDTO;
        }

        public CommentDTO Add(CommentDTO comment)
        {
            Comment dbcomment = _mapper.Map<Comment>(comment);
            dbcomment.CreationDate = DateTime.Now;
            _uow.Comments.Add(dbcomment);
            _uow.Save();
            return _mapper.Map<CommentDTO>(dbcomment);
        }

        public void Delete(int id)
        {
            _uow.Comments.Delete(id);
            Comment dbcomment = _uow.Comments.Get(id);
            _uow.Save();
        }

        public void Update(CommentDTO comment)
        {
            Comment dbcomment = _mapper.Map<Comment>(comment);
            _uow.Comments.Update(dbcomment);
            _uow.Save();
        }
    }
}
