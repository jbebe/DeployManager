using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Api.Helper
{
    public static class EnumHelper
    {
        public static IEnumerable<TResult> Select<T, TResult>(this T obj, Func<T, TResult> func) where T : Enum
        {
            foreach (T deployType in Enum.GetValues(typeof(T)))
            {
                yield return func(deployType);
            }
        }

        public static IEnumerable<T> Values<T>(this T obj) where T : Enum
        {
            foreach (T deployType in Enum.GetValues(typeof(T)))
            {
                yield return deployType;
            }
        }

        public static int NumericValue<T>(this T obj) where T : Enum => int.Parse(obj.ToString("D"));

        public static string StringValue<T>(this T obj) where T : Enum => obj.ToString("G");
    }
}
