using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestWebAPI.Models.IDs;

namespace TestWebAPI.Models;

[Table("Login")]
internal sealed record DB_Login
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public LoginId Id { get; init; }
    public string? UserName { get; internal set; }

    public required string Email { get; internal set; }
    public LoginStatus Status { get; internal set; }

    public required BrandId BrandId { get; internal set; }
    public DB_Brand? Brand { get; internal set; } = default!;
}