using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StronglyTypedIds;

namespace TestWebAPI.Models.IDs;

[StronglyTypedId]
public partial struct LoginId
{
    public static bool TryParse(ReadOnlySpan<char> input, out LoginId loginId)
    {
        if (Guid.TryParse(input, out var loginGuidId))
        {
            loginId = new(loginGuidId);
            return true;
        }

        loginId = default;
        return false;
    }

    public class StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null) : ValueConverter<LoginId, Guid>(id => id.Value, value => new LoginId(value), mappingHints)
    {
    }

    public static bool operator !=(LoginId a, object b) => b is not Guid g || a.Value != g;
    public static bool operator ==(LoginId a, object b) => a.Value.Equals(b);

}