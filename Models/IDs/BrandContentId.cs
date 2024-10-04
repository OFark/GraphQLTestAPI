using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StronglyTypedIds;

namespace TestWebAPI.Models.IDs;

[StronglyTypedId]
public partial struct BrandContentId
{
    public static bool TryParse(ReadOnlySpan<char> input, out BrandContentId contentId)
    {
        if (Guid.TryParse(input, out var contentGuidId))
        {
            contentId = new(contentGuidId);
            return true;
        }

        contentId = default;
        return false;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<BrandContentId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null) : base(id => id.Value, value => new BrandContentId(value), mappingHints)
        {
        }
    }
}