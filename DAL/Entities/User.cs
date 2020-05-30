using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;



namespace DAL.Entities
{
    public class User: IdentityUser<int>
    {
        public IEnumerable<Post> Posts { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PictureUrl { get; set; }
    }
}
