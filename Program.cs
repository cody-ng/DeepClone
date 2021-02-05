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


            CloneListTest(Helper.CreateItems(1));

            CloneListTest(Helper.CreateItems(10));

            //LinkedListItemTest.Equal_Value_Tests();

            LinkedListHandlerTest.IsUnique_Test();
            LinkedListHandlerTest.Equal_Test();
            LinkedListHandlerTest.Length_Test();
            LinkedListHandlerTest.GetLastItem_Test();
            LinkedListHandlerTest.GetNthItem_Test();

            LinkedListHandlerTest.Clone_LoopBack_List_Tests();

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

            //var isUnique = LinkedListHandlerTest.IsUnique(input, result);
            //Console.WriteLine($"isUnique = {isUnique}");
            //Console.WriteLine();
        }

#if false
        static void IsUniqueListTest()
        {
            var l1 = Helper.CreateItems(1);
            var l2 = Helper.CreateItems(3);
            var l3 = Helper.CreateItems(1000);
            var l4 = Helper.CreateItems(50);

            Console.WriteLine($"l1 and l1 are unique? = {LinkedListHandlerTest.IsUnique(l1, l1)}");
            Console.WriteLine($"l2 and l2 are unique? = {LinkedListHandlerTest.IsUnique(l2, l2)}");
            Console.WriteLine($"l3 and l3 are unique? = {LinkedListHandlerTest.IsUnique(l3, l3)}");
            Console.WriteLine($"l4 and l4 are unique? = {LinkedListHandlerTest.IsUnique(l4, l4)}");

            Console.WriteLine($"l1 and l2 are unique? = {LinkedListHandlerTest.IsUnique(l1, l2)}");
            Console.WriteLine($"l1 and l3 are unique? = {LinkedListHandlerTest.IsUnique(l1, l3)}");
            Console.WriteLine($"l1 and l4 are unique? = {LinkedListHandlerTest.IsUnique(l1, l4)}");

            Console.WriteLine($"l2 and l1 are unique? = {LinkedListHandlerTest.IsUnique(l2, l1)}");
            Console.WriteLine($"l2 and l3 are unique? = {LinkedListHandlerTest.IsUnique(l2, l3)}");
            Console.WriteLine($"l2 and l4 are unique? = {LinkedListHandlerTest.IsUnique(l2, l4)}");

            Console.WriteLine($"l3 and l1 are unique? = {LinkedListHandlerTest.IsUnique(l3, l1)}");
            Console.WriteLine($"l3 and l2 are unique? = {LinkedListHandlerTest.IsUnique(l3, l2)}");
            Console.WriteLine($"l3 and l4 are unique? = {LinkedListHandlerTest.IsUnique(l3, l4)}");

        }
#endif
    }
}
