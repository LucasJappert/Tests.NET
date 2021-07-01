using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestsLucas
{
    public static class HashingExtensions
    {
        public static string SHA256<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException();

            var serialized = JsonSerializer.Serialize(GetObjectWithoutExcludedProperties(source));
            var hash = new SHA256Managed().ComputeHash(
                Encoding.UTF8.GetBytes(serialized)
            );
            var ret = string.Concat(
                hash.Select(b => b.ToString("x2"))
            );
            return ret;
        }
        public static object GetObjectWithoutExcludedProperties<T>(this T source)
        {
            Type t = typeof(T);
            PropertyInfo[] filteredProps = t.GetProperties()
                                            .Where(s => s.GetCustomAttribute(typeof(CustomAttributes.HashIgnoreAttribute)) == null)
                                            .ToArray();
            var ret = new ExpandoObject() as IDictionary<string, Object>;
            foreach (var property in filteredProps)
            {
                ret[property.Name] = property.GetValue(source);
            }
            return ret;

        }
    }
}
