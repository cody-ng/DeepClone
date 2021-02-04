using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static public class LinkedListItemTest
    {


        public static void Equal_Value_Tests()
        {
            Console.WriteLine("Equality test...");
            var i1 = new LinkedListItem() { Id = 1, Name = "first" };
            var i2 = new LinkedListItem() { Id = 1, Name = "first" };
            LinkedListItem_Equal_Value_Test(i1, i2);

            i1 = new LinkedListItem() { Id = 1, Name = "" };
            i2 = new LinkedListItem() { Id = 1, Name = "" };
            LinkedListItem_Equal_Value_Test(i1, i2);

            i1 = new LinkedListItem() { Id = 1, Name = null };
            i2 = new LinkedListItem() { Id = 1, Name = null };
            LinkedListItem_Equal_Value_Test(i1, i2);

            Console.WriteLine();
            Console.WriteLine("InEquality test...");
            i1 = new LinkedListItem() { Id = 1, Name = "test" };
            i2 = new LinkedListItem() { Id = 1, Name = null };
            LinkedListItem_Equal_Value_Test(i1, i2);

            i1 = new LinkedListItem() { Id = 1, Name = "" };
            i2 = new LinkedListItem() { Id = 1, Name = null };
            LinkedListItem_Equal_Value_Test(i1, i2);

            i1 = new LinkedListItem() { Id = 2, Name = "test" };
            i2 = new LinkedListItem() { Id = 1, Name = "test" };
            LinkedListItem_Equal_Value_Test(i1, i2);

            i1 = new LinkedListItem() { Id = 1, Name = "test" };
            i2 = new LinkedListItem() { Id = 1, Name = "john" };
            LinkedListItem_Equal_Value_Test(i1, i2);

            Console.WriteLine();

        }
        static void LinkedListItem_Equal_Value_Test(LinkedListItem item1, LinkedListItem item2)
        {
            var s = $"item1={item1.ToString()}, item2={item2.ToString()}, are equal? = {item1.Equals(item2)}";
            Console.WriteLine(s);
        }



    }

}
