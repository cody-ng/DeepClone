using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;

namespace DeepClone
{
    class Program
    {
        static void Main(string[] args)
        {
            //int input = 1;
            //string input = "abc";
            //var input = new Book();
            var input = new BaseTestClass() { Id=1, Name="test", Count=10};

            var result = DeepClone.Clone(input);
            Console.WriteLine($"result = {result}");

            Console.WriteLine($"is clone referenceEqual() to input? = {object.ReferenceEquals(input, result)}");
            
        }
    }
}
