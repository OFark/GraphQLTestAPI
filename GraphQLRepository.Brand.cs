using Mapster;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Interfaces;
using TestWebAPI.Models.IDs;
using BrandRead = TestWebAPI.Schema.Queries.BrandRead;

namespace Sera.Infrastructure.SQL.Repositories;

internal sealed partial class GraphQLRepository : IGraphQLRepository
{
    public IQueryable<BrandRead> GetBrands() 
        => dbContext.Brands.ProjectToType<BrandRead>();

    public async Task<BrandRead?> GetBrand(BrandId brandId, CancellationToken cancellation = default) 
        => await GetBrands().FirstOrDefaultAsync(b => b.Id == brandId, cancellation).ConfigureAwait(false);
}