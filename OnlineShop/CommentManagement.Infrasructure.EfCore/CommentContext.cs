using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore
{
    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
