using System.Security.Claims;
using TestWebAPI.Interfaces;

namespace TestWebAPI.Schema.Queries;
public sealed class Query
{
    public User Me(ClaimsPrincipal principal)
    {
        var user = new User()
        {
            Name = principal.Identity?.Name
        };

        return user;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<LoginRead> GetLogins(IGraphQLRepository graphQLRepository, ClaimsPrincipal principal)
            => graphQLRepository.GetLogins<LoginRead>();
}

public class User()
{
    public string Name { get; init;  }
}