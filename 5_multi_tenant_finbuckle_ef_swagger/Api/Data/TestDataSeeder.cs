using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Api.TenantFinbuckle;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Data
{
    public class TestDataSeeder
    {
        private IConfiguration configuration;

        public TestDataSeeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SeedData()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingDbContext>();
            optionsBuilder.UseSqlite("Data Source=blogging.db");
            List<JwtTenantInfo> tenants = configuration
                .GetSection("Finbuckle:MultiTenant:Stores:ConfigurationStore:Tenants")
                .Get<List<JwtTenantInfo>>();
            // Console.WriteLine(JsonSerializer.Serialize(tenants));

            var tenant0Context = new BloggingDbContext(tenants[0], optionsBuilder.Options);
            if (!tenant0Context.Blogs.Any()) {
                var myBlog = new Blog {
                    Owner = "Mr. X",
                };
                tenant0Context.Blogs.Add(myBlog);
                tenant0Context.SaveChanges();
            }

            var tenant1Context = new BloggingDbContext(tenants[1], optionsBuilder.Options);
            if (!tenant1Context.Blogs.Any()) {
                var myBlog = new Blog {
                    Owner = "Mr. Y",
                };
                tenant1Context.Blogs.Add(myBlog);
                tenant1Context.SaveChanges();
            }
        }
    }
}