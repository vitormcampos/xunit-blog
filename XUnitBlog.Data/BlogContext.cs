using System.Reflection;
using Microsoft.EntityFrameworkCore;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Data;

internal class BlogContext : DbContext
{
    public BlogContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
