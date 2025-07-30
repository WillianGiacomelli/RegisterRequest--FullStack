using Microsoft.EntityFrameworkCore;
using requestpAPI.Models;

namespace requestpAPI.Data
{
    public class RequestContext : DbContext
    {
        public RequestContext(DbContextOptions<RequestContext> options) : base(options)
        {
        }
        public DbSet<Request> Request { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Request)
                .HasForeignKey(i => i.RequestId);

            modelBuilder.Entity<RequestItem>()
               .Property(p => p.ProductValue)
               .HasColumnType("decimal(18,2)");
        }
    }
}
