using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestWebAPI.Models.IDs;

namespace TestWebAPI.Models;

[Table("BrandContent")]
internal sealed record DB_BrandContent
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public BrandContentId Id { get; internal set; }

    public required BrandId BrandId { get; internal set; }
    public DB_Brand Brand { get; internal set; } = default!;

    public required string ContentText { get; internal set; }

}