namespace Infrastructure.Models;

public class ContactModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Question { get; set; } = null!;
}
