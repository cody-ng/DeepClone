using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using DeepClone.TestCases;
using System;

namespace DeepClone
{
    class Program
    {
        static void Main(string[] args)
        {
            // value type tests
            CloneTest(ValueTypeTest.CreateInteger());

            CloneTest(ValueTypeTest.CreateBook());


            // reference type tests
            CloneTest(ReferenceTypeTest.CreateString());

            CloneTest(ReferenceTypeTest.CreateBaseTestClass());
            
            CloneTest(ReferenceTypeTest.CreateDerivedTestClass());


            CloneListTest(LinkedListTest.CreateItems(1));

            CloneListTest(LinkedListTest.CreateItems(10));
            
        }

        static void CloneTest(object input)
        {
            var result = DeepClone.Clone(input);
            Console.WriteLine($"result = {result}");

            Console.WriteLine($"is clone referenceEqual() to input? = {object.ReferenceEquals(input, result)}");
            Console.WriteLine();
        }


        static void CloneListTest(LinkedListItem input)
        {
            var result = (LinkedListItem)DeepClone.Clone(input);
            Console.WriteLine($"result = {result}");

            Console.WriteLine($"is clone referenceEqual() to input? = {object.ReferenceEquals(input, result)}");

            var isUnique = LinkedListTest.IsUnique(input, result);
            Console.WriteLine($"isUnique = {isUnique}");
            Console.WriteLine();
        }

    }
}
