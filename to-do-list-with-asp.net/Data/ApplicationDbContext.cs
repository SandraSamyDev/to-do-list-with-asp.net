using Microsoft.EntityFrameworkCore;
using to_do_list_with_asp.net_.Models;

namespace to_do_list_with_asp.net_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}