using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Data.EntityConfigurations;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(p => p.Title).HasColumnName("title").IsRequired().HasMaxLength(200);

        builder.Property(p => p.Content).HasColumnName("content").HasColumnType("text");

        builder.Property(p => p.Thumbnail).HasColumnName("thumbnail").HasMaxLength(500);

        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();

        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder
            .Property(p => p.PostStatus)
            .HasColumnName("post_status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();

        builder
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
