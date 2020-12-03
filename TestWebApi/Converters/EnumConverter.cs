
using TestWebApi.Infrastructure.Models.Enums;

namespace TestWebApi.Converters
{
    public static class EnumConverter
    {
        public static string GetKeyName(this StateStatus status) => status.ToString("g").ToLower();
    }
}
