using Microsoft.EntityFrameworkCore;
using TestWebAPI;
using TestWebAPI.Interfaces;

namespace Sera.Infrastructure.SQL.Repositories;

internal sealed partial class GraphQLRepository(ILogger<GraphQLRepository> logger, IDbContextFactory<dBContext> dbContextFactory) : IGraphQLRepository, IAsyncDisposable
{
    private readonly dBContext dbContext = dbContextFactory.CreateDbContext();

    public ValueTask DisposeAsync() => dbContext.DisposeAsync();

}