using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public UserDTO Author { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        public bool Anonymous { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
