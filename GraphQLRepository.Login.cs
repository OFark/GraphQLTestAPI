using Mapster;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Interfaces;
using TestWebAPI.Models;
using TestWebAPI.Models.IDs;

namespace Sera.Infrastructure.SQL.Repositories;

internal sealed partial class GraphQLRepository : IGraphQLRepository
{
    public IQueryable<TResponse> GetLogins<TResponse>() 
        => MyLogins().ProjectToType<TResponse>();

    public async Task<TResponse?> GetLogin<TResponse>(LoginId loginId, CancellationToken cancellation = default)
        => await MyLogins().Where(x => x.Id == loginId)
                                            .ProjectToType<TResponse>()
                                            .FirstOrDefaultAsync(cancellation)
                                            .ConfigureAwait(false);

    
    private IQueryable<DB_Login> MyLogins() => dbContext.Logins;
}