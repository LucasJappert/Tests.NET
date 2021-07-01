using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsLucas
{
    public static class CustomAttributes
    {

        [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
        public sealed class HashIgnoreAttribute : Attribute
        {
            public HashIgnoreAttribute() { }
        }
    }
}
