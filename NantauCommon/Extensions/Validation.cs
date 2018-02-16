using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantauCommon.Extensions
{
    public static class Validation
    {
        public static void NotNull(this object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name);
        }

        public static void AssignIfNotNull<T>(this T obj, T other, string name) where T : class
        {
            obj = other ?? throw new ArgumentNullException(name);
        }
    }
}
