using System;
using System.Collections.Generic;

namespace Common.Utils
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
    }
}
