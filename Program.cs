using System;

namespace DeepClone
{
    class Program
    {
        static void Main(string[] args)
        {
            //int input = 1;
            string input = "abc";

            var result = DeepClone.Clone(input);
            Console.WriteLine($"result = {result}");

            Console.WriteLine($"is clone referenceEqual() to input? = {object.ReferenceEquals(input, result)}");
            
        }
    }
}
