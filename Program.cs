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
            DogConsoleWriter.Line(new Dog
            {
                Name = "Astro",
                Breed = "Newfoundland"
            });
        }
    }

    class Dog
    {
        [Color(ConsoleColor.Red)]
        public string Name { get; set; }
        [Color]
        public string Breed { get; set; }
        [Color(ConsoleColor.Yellow)]
        public int Age { get; set; }

        public 
    }
    static class DogConsoleWriter
    {
        public static void Line(Dog dog)
        {
            var defaultColor = Console.ForegroundColor;
            Console.Write("Name: ");
            // Here the console foreground is set to either the attribute color or a default color
            Console.ForegroundColor = GetPropertyColor(nameof(Dog.Name)) ?? defaultColor; ;
            Console.Write(dog.Name);
            Console.ForegroundColor = defaultColor;
            Console.WriteLine();

            Console.Write("Breed: ");
            Console.ForegroundColor = GetPropertyColor(nameof(Dog.Breed)) ?? defaultColor;
            Console.Write(dog.Breed);
            Console.ForegroundColor = defaultColor;
            Console.WriteLine();

            Console.Write("Age: ");
            Console.ForegroundColor = GetPropertyColor(nameof(Dog.Age)) ?? defaultColor;
            Console.Write(dog.Age);
            Console.ForegroundColor = defaultColor;
            Console.WriteLine();
        }

        // Here is the most important part
        private static ConsoleColor? GetPropertyColor(string propertyName)
        {
            // This uses C#'s reflection to get the attribute if one exists
            PropertyInfo propertyInfo = typeof(Dog).GetProperty(propertyName);
            ColorAttribute colorAttribute = (ColorAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ColorAttribute));

            // If colorAttribute is not null, than a color attribute exists
            if (colorAttribute != null)
            {
                return colorAttribute.Color;
            }
            return null;
        }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    sealed class ColorAttribute : Attribute
    {
        public ColorAttribute(ConsoleColor color = ConsoleColor.Cyan)
        {
            Color = color;
        }

        public ConsoleColor Color { get; }
    }
}
