using Microsoft.EntityFrameworkCore;

namespace PRSCGraham.Models
{
    public class PrsDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLines> RequestLines { get; set; }
        //Vendors
        public PrsDbContext(DbContextOptions<PrsDbContext> options) : base(options) { }
    }
}
