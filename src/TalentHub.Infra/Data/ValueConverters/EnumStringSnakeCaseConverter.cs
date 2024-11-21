using Humanizer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.Infra.Data.ValueConverters;

public class EnumStringSnakeCaseConverter<TEnum> :
    ValueConverter<TEnum, string>
    where TEnum : struct, Enum
{
    public EnumStringSnakeCaseConverter()
        : base(
            v => v.ToString().Underscore(),
            v => (TEnum)Enum.Parse(typeof(TEnum), v.Pascalize()))
    {
    }
}
