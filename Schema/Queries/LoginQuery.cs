using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using TestWebAPI.Interfaces;

namespace TestWebAPI.Schema.Queries;

[ExtendObjectType(typeof(LoginRead))]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "HotChocolate 'fields' cannot be marked as static and still be visible")]
public sealed class LoginQuery
{
    [BindMember(nameof(LoginRead.BrandId))]
    public async Task<BrandRead?> GetBrand([Parent] LoginRead login, ClaimsPrincipal principal, IGraphQLRepository graphQLRepository, CancellationToken cancellation = default)
        => await graphQLRepository.GetBrand(login.BrandId, cancellation).ConfigureAwait(false);
}