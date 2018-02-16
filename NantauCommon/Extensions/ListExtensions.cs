using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NantauCommon.Extensions
{
    static class ListExtensions
    {
        public static T PopFront<T>(this List<T> list)
        {
            var popped = list.First();
            list.RemoveAt(0);
            return popped;
        }

        public static T PopBack<T>(this List<T> list)
        {
            var popped = list.Last();
            list.RemoveAt(list.Count - 1);
            return popped;
        }
    }
}
