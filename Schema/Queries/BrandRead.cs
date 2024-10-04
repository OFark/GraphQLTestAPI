using TestWebAPI.Models.IDs;

namespace TestWebAPI.Schema.Queries;
public sealed record BrandRead
{
    public BrandId Id { get; init; }

    public required string Name { get; init; }

    [UseFiltering]
    public ICollection<BrandContentRead>? Contents { get; init; }
}
