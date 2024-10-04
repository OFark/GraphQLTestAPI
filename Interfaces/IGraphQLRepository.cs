using TestWebAPI.Models.IDs;
using TestWebAPI.Schema.Queries;

namespace TestWebAPI.Interfaces;

public interface IGraphQLRepository
{
    Task<BrandRead?> GetBrand(BrandId brandId, CancellationToken cancellation = default);

    IQueryable<BrandRead> GetBrands();

    Task<TResponse?> GetLogin<TResponse>(LoginId loginId, CancellationToken cancellation = default);

    IQueryable<TResponse> GetLogins<TResponse>();
}