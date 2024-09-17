using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecu_onatrix_contactProvider.Data.Entities;

public class ContactEntity
{
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "nvarchar(20)")]
    public string? Phone { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string Category { get; set; } = null!;
}
