using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <remarks>
/// Credito: https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp
/// </remarks>
namespace TestsLucas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Type t = typeof(Dog);
            //// Get the public properties.
            //PropertyInfo[] props = t.GetProperties();
            //Console.WriteLine("The number of public properties: {0}.\n", props.Length);

            //PropertyInfo[] filteredProps = props.Where(s => s.GetCustomAttribute(typeof(HashIgnoreAttribute)) == null).ToArray();
            //Console.WriteLine("The number of public properties: {0}.\n", filteredProps.Length);


            Dog dog1 = new Dog() { Age = 21, Breed = "Pequine", Name = "Jhon1" };
            Dog dog2 = new Dog() { Age = 22, Breed = "Pequine", Name = "Jhon1" };

            var hash11 = dog1.GetObjectWithoutExcludedProperties();
            var hash22 = dog2.GetObjectWithoutExcludedProperties();

            var hash111 = JsonSerializer.Serialize(hash11);
            var hash222 = JsonSerializer.Serialize(hash22);
            bool aasdasdsas111 = (hash111 == hash222);

            var a1 = new SHA256Managed().ComputeHash(
                Encoding.UTF8.GetBytes(hash111)
            );
            var a2 = new SHA256Managed().ComputeHash(
                Encoding.UTF8.GetBytes(hash222)
            );
            bool a3 = (a1 == a2);

            var b1 = string.Concat(
                a1.Select(b => b.ToString("x2"))
            );
            var b2 = string.Concat(
                a2.Select(b => b.ToString("x2"))
            );
            bool b3 = (b1 == b2);

            var hash1 = dog1.SHA256();
            var hash2 = dog2.SHA256();
            bool aasdasdsas1 = (hash1 == hash2);
            //44136fa355b3678a1146ad16f7e8649e94fb4fc21fe77e8310c060f61caaff8a
            Console.WriteLine(aasdasdsas1);
            //var objectReduced = dog.GetObjectWithoutExcludedProperties();
        }

    }
}
