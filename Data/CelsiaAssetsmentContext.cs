using Microsoft.EntityFrameworkCore;
using celsiaAssetsment.Models;

namespace celsiaAssetsment.Data
{
    public class CelsiaAssetsmentContext : DbContext
    {
        public CelsiaAssetsmentContext (DbContextOptions<CelsiaAssetsmentContext> options) : base (options) {}

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}