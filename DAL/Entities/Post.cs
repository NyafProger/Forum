using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Anonymous { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
