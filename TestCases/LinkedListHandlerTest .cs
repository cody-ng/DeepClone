using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static public class LinkedListHandlerTest
    {

#if false
        // compare 2 list items to ensure every item is unique
        // (there are no shared items between the 2 lists)
        public static bool IsUnique(LinkedListItem list1, LinkedListItem list2)
        {
            var hash = new HashSet<LinkedListItem>(); // 1st list
            var traversedHash = new HashSet<LinkedListItem>(); //2nd list
            var temp = list1;
            try
            {
                // 1) put list1's items into hash
                while (temp != null)
                {
                    // check for cyclic loop.  Once loop is detected, break
                    if (hash.Contains(temp))
                        break;

                    hash.Add(temp);
                    temp = temp.Next;
                }

                // 2) make sure no items in list2 appears in list1's hash
                temp = list2;
                while (temp != null)
                {
                    // check for cyclic loop.  Once loop is detected, break
                    if (traversedHash.Contains(temp))
                        break;
                    traversedHash.Add(temp);

                    // if an item in list2 appears in the list1's hash, then 
                    // the 2 lists are not unique.
                    if (hash.Contains(temp))
                        return false;

                    temp = temp.Next;
                }
            }
            finally
            {
                hash.Clear();
                traversedHash.Clear();
            }

            return true; // no duplicates => unique
        }

#endif

        public static void IsUniqueTest()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(3);
            var l3 = Helper.CreateItemHandlers(1000);
            var l4 = Helper.CreateItemHandlers(50);

            Console.WriteLine($"l1 and l1 are unique? = {l1.IsUnique(l1)}");
            Console.WriteLine($"l2 and l2 are unique? = {l2.IsUnique(l2)}");
            Console.WriteLine($"l3 and l3 are unique? = {l3.IsUnique(l3)}");
            Console.WriteLine($"l4 and l4 are unique? = {l4.IsUnique(l4)}");
            Console.WriteLine();

            Console.WriteLine($"l1 and l2 are unique? = {l1.IsUnique(l2)}");
            Console.WriteLine($"l1 and l3 are unique? = {l1.IsUnique(l3)}");
            Console.WriteLine($"l1 and l4 are unique? = {l1.IsUnique(l4)}");
            Console.WriteLine();

            Console.WriteLine($"l2 and l1 are unique? = {l2.IsUnique(l1)}");
            Console.WriteLine($"l2 and l3 are unique? = {l2.IsUnique(l3)}");
            Console.WriteLine($"l2 and l4 are unique? = {l2.IsUnique(l4)}");
            Console.WriteLine();

            Console.WriteLine($"l3 and l1 are unique? = {l3.IsUnique(l1)}");
            Console.WriteLine($"l3 and l2 are unique? = {l3.IsUnique(l2)}");
            Console.WriteLine($"l3 and l4 are unique? = {l3.IsUnique(l4)}");
            Console.WriteLine();
        }

        public static void EqualTest()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(3);
            var l3 = Helper.CreateItemHandlers(1000);
            var l4 = Helper.CreateItemHandlers(500);
            var l5 = Helper.CreateItemHandlers(500);

            Console.WriteLine("Not equal cases...");
            Console.WriteLine($"l1 and l2 are equal? = {l1.AreEqual(l2)}");
            Console.WriteLine($"l2 and l3 are equal? = {l2.AreEqual(l3)}");
            Console.WriteLine($"l3 and l4 are equal? = {l3.AreEqual(l4)}");
            Console.WriteLine();

            Console.WriteLine("Equal cases...");
            Console.WriteLine($"l1 and l1 are equal? = {l1.AreEqual(l1)}");
            Console.WriteLine($"l2 and l2 are equal? = {l2.AreEqual(l2)}");
            Console.WriteLine($"l4 and l5 are equal? = {l4.AreEqual(l5)}");

        }

    }

}
