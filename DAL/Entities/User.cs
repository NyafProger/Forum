using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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
