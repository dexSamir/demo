using System;
using System.Reflection;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(Student);
            var field = type.GetField("Id", BindingFlags.NonPublic|BindingFlags.Instance);
            Console.WriteLine(field);
            
        }
    }
}   
