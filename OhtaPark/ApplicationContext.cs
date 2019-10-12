using System.Data.Entity;
using static OhtaPark.MainWindow;

namespace OhtaPark
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Client> Clients { get; set; }
    }
}
