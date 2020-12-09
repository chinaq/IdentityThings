using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class BloggingDbContext : MultiTenantDbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingDbContext(ITenantInfo tenantInfo) : base(tenantInfo) { }

        public BloggingDbContext(ITenantInfo tenantInfo, DbContextOptions<BloggingDbContext> options) :
            base(tenantInfo, options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure an entity type to be multitenant.
            builder.Entity<Blog>().IsMultiTenant();
            // builder.Entity<Post>().IsMultiTenant();
        }
    }
}