using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using DeployManager.Commons;

namespace DeployManager.Api.Helper
{
    public static class ApiDateExtension
    {
        private const string ApiDateFormat = "yyyyMMddhhmmss";

        public static string ToApiDate(this DateTime date) => date.ToString(ApiDateFormat);

        public static DateTime? ParseApiDate(this string date)
        {
            var isParsed = DateTime.TryParseExact(date, ApiDateFormat, 
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var parsed);

            if (!isParsed)
            {
                return null;
            }

            return parsed;
        }

        public static string ToApiDuration(this TimeSpan timeSpan) => XmlConvert.ToString(timeSpan);

        public static TimeSpan? ParseApiDuration(this string timespan)
        {
            try
            {
                return XmlConvert.ToTimeSpan(timespan);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
