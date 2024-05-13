using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set;} = null!;

        public DbSet<Role> Roles { get; set;} = null!;

        public DbSet<Product> Products { get; set;} = null!;

        public DbSet<Receipt> Receipts { get; set;} = null!;

        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}