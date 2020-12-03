using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Converters
{
    public static class DateTimeConverter
    {
        public static string ToStringISO8601(this DateTime dateTime)
            => dateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);
    }
}
