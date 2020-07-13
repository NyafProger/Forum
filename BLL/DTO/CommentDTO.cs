using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public UserDTO Author { get; set; }
        [Required]
        public string Text { get; set; }
        public bool Anonymous { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
