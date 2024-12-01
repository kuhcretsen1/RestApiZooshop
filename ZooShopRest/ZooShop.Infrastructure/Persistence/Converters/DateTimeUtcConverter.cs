using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ZooShop.Infrastructure.Converters
{
    public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeUtcConverter() 
            : base(
                x => x.ToUniversalTime(), 
                x => x.Kind == DateTimeKind.Unspecified 
                    ? DateTime.SpecifyKind(x, DateTimeKind.Utc) 
                    : x.ToUniversalTime())
        { }
    }
}