using System;
using System.Reflection;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the assembly.");
            return;
        }

        string assemblyPath = args[0];

        try
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            var attribute = (GuidAttribute)assembly.GetCustomAttribute(typeof(GuidAttribute));

            if (attribute != null)
            {
                Console.WriteLine($"GUID: {attribute.Value}");
            }
            else
            {
                Console.WriteLine("No GUID found for the given assembly.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
