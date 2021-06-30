using System;
using System.Reflection;

/// <remarks>
/// Credito: https://www.pluralsight.com/guides/how-to-create-custom-attributes-csharp
/// </remarks>
namespace TestsLucas
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(Dog);
            // Get the public properties.
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine("The number of public properties: {0}.\n",
                              propInfos.Length);
            // Display the public properties.
            DisplayPropertyInfo(propInfos);

            // Get the nonpublic properties.
            PropertyInfo[] propInfos1 = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine("The number of non-public properties: {0}.\n",
                              propInfos1.Length);
            // Display all the nonpublic properties.
            DisplayPropertyInfo(propInfos1);
        }
        public static void DisplayPropertyInfo(PropertyInfo[] propInfos)
        {
            // Display information for all properties.
            foreach (var propInfo in propInfos)
            {
                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;

                Console.WriteLine("   Property name: {0}", propInfo.Name);
                Console.WriteLine("   Property type: {0}", propInfo.PropertyType);
                Console.WriteLine("   Read-Write:    {0}", readable & writable);
                if (readable)
                {
                    MethodInfo getAccessor = propInfo.GetMethod;
                    Console.WriteLine("   Visibility:    {0}",
                                      GetVisibility(getAccessor));
                }
                if (writable)
                {
                    MethodInfo setAccessor = propInfo.SetMethod;
                    Console.WriteLine("   Visibility:    {0}",
                                      GetVisibility(setAccessor));
                }
                Console.WriteLine();
            }
        }
        public static String GetVisibility(MethodInfo accessor)
        {
            if (accessor.IsPublic)
                return "Public";
            else if (accessor.IsPrivate)
                return "Private";
            else if (accessor.IsFamily)
                return "Protected";
            else if (accessor.IsAssembly)
                return "Internal/Friend";
            else
                return "Protected Internal/Friend";
        }
    }

    class Dog
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        [ExcludeToHash()]
        public int Age { get; set; }

        public void ToHash()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    sealed class ExcludeToHashAttribute : Attribute
    {
        public ExcludeToHashAttribute() { }
    }
}
