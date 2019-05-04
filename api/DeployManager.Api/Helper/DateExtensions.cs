using System;
using System.Globalization;

namespace DeployManager.Api.Helper
{
    public static class ApiDateExtension
    {
        private const string ApiDateFormat = "yyyyMMddhhmmss";

        public static string ToApiString(this DateTime date) => date.ToString(ApiDateFormat);

        public static DateTime? ParseApiString(this string date)
        {
            var isParsed = DateTime.TryParseExact(date, ApiDateFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var parsed);

            if (!isParsed)
            {
                return null;
            }

            return parsed;
        }
    }
}
