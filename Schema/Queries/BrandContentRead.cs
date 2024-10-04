using TestWebAPI.Models.IDs;

namespace TestWebAPI.Schema.Queries;
public sealed record BrandContentRead
{
    public required BrandContentId Id { get; init; }

    public BrandRead? Brand { get; init; } = default!;
    public required BrandId BrandId { get; init; }
    public required string ContentText { get; init; }
}
