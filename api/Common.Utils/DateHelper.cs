using Newtonsoft.Json;
using System;

namespace Common.Utils
{
    public class DateHelper
    {
        public class DateTimeInterval
        {
            public DateTime From { get; }
            public DateTime To { get; }

            public TimeSpan Interval => To - From;

            [JsonConstructor]
            public DateTimeInterval(DateTime from, DateTime to)
            {
                From = from;
                To = to;
            }

            public DateTimeInterval(DateTime from, TimeSpan amount)
            {
                From = from;
                To = from + amount;
            }
        }
    }
}
