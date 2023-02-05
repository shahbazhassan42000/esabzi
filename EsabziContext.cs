using esabzi.Models;
using Microsoft.EntityFrameworkCore;

namespace esabzi.DB;

public partial class EsabziContext : DbContext
{
    public EsabziContext()
    {
    }

    public EsabziContext(DbContextOptions<EsabziContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ESABZI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
}
