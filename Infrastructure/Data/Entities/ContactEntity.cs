using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecu_onatrix_contactProvider.Data.Entities;

public class ContactEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(128)")]
    public string? Name { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(128)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "nvarchar(20)")]
    public string? Phone { get; set; }

    [Column(TypeName = "nvarchar(256)")]
    public string? Category { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Question { get; set; }
}
