using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class PostConfiguration: IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.Id);
            builder.Property(post => post.Text).IsRequired();
            builder.HasOne(post => post.Author)
                .WithMany(author => author.Posts)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
