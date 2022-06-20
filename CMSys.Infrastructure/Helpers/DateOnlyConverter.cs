using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CMSys.Infrastructure.Helpers;

/// <summary>
///     Converts <see cref="DateOnly" /> to <see cref="DateTime" /> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(d => d.ToDateTime(TimeOnly.MinValue), d => DateOnly.FromDateTime(d))
    {
    }
}