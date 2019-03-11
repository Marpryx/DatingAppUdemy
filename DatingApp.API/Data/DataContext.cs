using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext //: inherit from
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Value>Values { get; set; }

    }
}