using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployManager.Commons
{
    public static class EnumerableExtensions
    {
        public static void Deconstruct<T>(this T[] array, out T first, out T[] rest)
        {
            first = array.Length > 0 ? array[0] : default;
            rest = array.Skip(1).ToArray();
        }

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T[] rest)
            => (first, (second, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T[] rest)
            => (first, second, (third, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T[] rest)
            => (first, second, third, (fourth, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth, out T[] rest)
            => (first, second, third, fourth, (fifth, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T[] rest)
            => (first, second, third, fourth, fifth, (sixth, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out T[] rest)
            => (first, second, third, fourth, fifth, sixth, (seventh, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out T eighth, out T[] rest)
            => (first, second, third, fourth, fifth, sixth, seventh, (eighth, rest)) = array;

        public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out T eighth, out T nineth, out T[] rest)
            => (first, second, third, fourth, fifth, sixth, seventh, eighth, (nineth, rest)) = array;
    }
}
