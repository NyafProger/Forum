using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.Configuration
{
    public class CommentConfiguration: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(comment => comment.Id);
            builder.Property(comment => comment.Text).IsRequired();
            builder.HasOne(comment => comment.Author);
            builder.HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
