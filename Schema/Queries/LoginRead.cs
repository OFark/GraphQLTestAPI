using TestWebAPI.Models;
using TestWebAPI.Models.IDs;

namespace TestWebAPI.Schema.Queries;

public sealed record LoginRead
{
    public LoginId Id { get; init; }
    public string? UserName { get; init; }
    public required string Email { get; init; }
    public LoginStatus Status { get; init; }
    public BrandId BrandId { get; set; }
}