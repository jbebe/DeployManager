using System;
using System.Collections.Generic;
using System.Text;

namespace DeployManager.Commons
{
    public class DateInterval
    {
        public DateInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Duration => End - Start;
    }

    public static class DateExtensions
    {
        public static DateTime Max(this DateTime a, DateTime b) => a > b ? a : b;
    }
}
