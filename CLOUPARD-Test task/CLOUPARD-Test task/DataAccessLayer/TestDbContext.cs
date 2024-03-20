using CLOUPARD_Test_task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CLOUPARD_Test_task.DAL
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
    }
}
