using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public UserDTO Author { get; set; }
        [Required]
        public string Text { get; set; }
        public bool Anonymous { get; set; }
        [Required]
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
