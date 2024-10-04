using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestWebAPI.Models.IDs;

namespace TestWebAPI.Models;

[Table("Brand"), Index(nameof(Name), IsUnique = true)]
internal sealed record DB_Brand
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public BrandId Id { get; internal set; }

    [MaxLength(256)]
    public required string Name { get; internal set; }
    public ICollection<DB_BrandContent>? Contents { get; internal set; }
    public ICollection<DB_Login>? Logins { get; internal set; }
}