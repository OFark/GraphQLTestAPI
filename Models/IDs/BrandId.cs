using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StronglyTypedIds;

namespace TestWebAPI.Models.IDs;

[StronglyTypedId]
public partial struct BrandId
{
    public static bool TryParse(ReadOnlySpan<char> input, out BrandId brandId)
    {
        if (Guid.TryParse(input, out var brandGuidId))
        {
            brandId = new(brandGuidId);
            return true;
        }

        brandId = default;
        return false;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<BrandId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null) : base(id => id.Value, value => new BrandId(value), mappingHints)
        {
        }
    }

}