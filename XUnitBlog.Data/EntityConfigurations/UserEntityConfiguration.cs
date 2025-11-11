using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Data.EntityConfigurations;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder
            .Property(u => u.FirstName)
            .HasColumnName("firstname")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName).HasColumnName("lastname").IsRequired().HasMaxLength(100);

        builder.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(255);

        builder.Property(u => u.Password).HasColumnName("password").IsRequired().HasMaxLength(255);

        builder.Property(u => u.UserName).HasColumnName("username").IsRequired().HasMaxLength(100);

        builder.Property(u => u.Photo).HasColumnName("photo").IsRequired(false).HasMaxLength(500);

        builder
            .Property(u => u.Role)
            .HasColumnName("role")
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasIndex(u => u.UserName).IsUnique();
    }
}
