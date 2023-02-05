namespace esabzi.Models;

public partial class User:FullAuditModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? ContactNo { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? Picture { get; set; }

    public string? Role { get; set; }

    
}
