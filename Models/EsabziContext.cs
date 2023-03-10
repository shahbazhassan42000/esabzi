using Microsoft.EntityFrameworkCore;

namespace esabzi.Models;

public partial class EsabziContext : DbContext
{
    public EsabziContext(){}

    public EsabziContext(DbContextOptions<EsabziContext> options)
        : base(options){}

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ESABZI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    /*=> optionsBuilder.UseSqlServer("Server = db; Database = master; User Id = SA; Password = hum-2977;MultipleActiveResultSets=true");*/
    /*=> optionsBuilder.UseSqlServer(@"Server = localhost, 1440; Database = master; User = sa; Password = Docker123!;");*/

    //setting logged in user
    private string _systemUserId = "2fd28110-93d0-427d-9207-d55dbca680fa";
    private string _loggedInUserId = "e2eb8989-a81a-4151-8e86-eb95a7961da2";

    //overriding SaveChanges to inject change tracking
    public override int SaveChanges()
    {
        var tracker = ChangeTracker;

        foreach (var entry in tracker.Entries())
        {
            if (entry.Entity is FullAuditModel)
            {
                var referenceEntity = entry.Entity as FullAuditModel;
                switch (entry.State)
                {
                    case EntityState.Added:
                        referenceEntity.CreatedDate = DateTime.Now;
                        referenceEntity.IsActive = true;

                        if (string.IsNullOrWhiteSpace(referenceEntity.CreatedByUserId))
                        {
                            referenceEntity.CreatedByUserId = _systemUserId;
                        }
                        break;
                    case EntityState.Deleted:
                    case EntityState.Modified:
                        referenceEntity.LastModifiedDate = DateTime.Now;
                        if (string.IsNullOrWhiteSpace(referenceEntity.LastModifiedUserId))
                        {
                            referenceEntity.LastModifiedUserId = _systemUserId;
                        }
                        break;
                    default:
                        break;
                }

            }
        }
        return base.SaveChanges();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);

        User user = new User
        {
            Id = 1,
            Name = "Shahbaz",
            Email = "shahbazhassan42000@gmail.com",
            ContactNo = "+923354058294",
            Username = "shahbaz",
            Password = "hum-2977",
            Address = "Street #3, House #22, near Data Darbar, Lahore",
            Picture = "https://i.ibb.co/HYJWqBc/Whats-App-Image-2022-10-19-at-23-57-52.jpg",
            Role = "ADMIN",
            CreatedByUserId = _systemUserId,
            CreatedDate = DateTime.Now,
            IsActive = true

        };
        user.EncryptPassword();

        modelBuilder.Entity<User>().HasData(user);
    }

}
