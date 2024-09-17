namespace Infrastructure.Models;

public class ContactResult
{
    public ContactModel? ContactRequest { get; set; }
    public IEnumerable<ContactModel>? ContactRequests { get; set; }
    public string Status { get; set; } = null!;
    public string? Error { get; set; }
}
