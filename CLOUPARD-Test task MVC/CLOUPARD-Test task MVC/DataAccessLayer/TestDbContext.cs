using CLOUPARD_Test_task_MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace CLOUPARD_Test_task_MVC.DataAccessLayer
{
    public class TestDbContext : DbContext
    {
        public  DbSet<Product> Product { get; set; }
        public TestDbContext(DbContextOptions<TestDbContext> options) :
            base(options)
        {
            Database.Migrate();
        }
    }
}
