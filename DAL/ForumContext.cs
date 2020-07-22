using DAL.Entities;
using DAL.Entities.Configuration;
using DAL.Entities.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ForumContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>() { Id = 3, Name = "Moderator", NormalizedName = "MODERATOR" });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>() { Id = 2, Name = "User", NormalizedName = "USER" });
            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName="ADMIN"});
        }
    }
}
