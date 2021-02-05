using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using DeepClone.ReferenceType;
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

        #region CreateItemHandlers sanity test

        public static void IsUnique_Test()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(3);
            var l3 = Helper.CreateItemHandlers(1000);
            var l4 = Helper.CreateItemHandlers(50);

            Console.WriteLine($"l1 and l1 are NOT unique? = {!l1.IsUnique(l1)}");
            Console.WriteLine($"l2 and l2 are NOT unique? = {!l2.IsUnique(l2)}");
            Console.WriteLine($"l3 and l3 are NOT unique? = {!l3.IsUnique(l3)}");
            Console.WriteLine($"l4 and l4 are NOT unique? = {!l4.IsUnique(l4)}");
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

        public static void Equal_Test()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(3);
            var l3 = Helper.CreateItemHandlers(1000);
            var l4 = Helper.CreateItemHandlers(500);
            var l5 = Helper.CreateItemHandlers(500);

            Console.WriteLine("Equal cases...");
            Console.WriteLine($"l1 and l1 are equal? = {l1.AreEqual(l1)}");
            Console.WriteLine($"l2 and l2 are equal? = {l2.AreEqual(l2)}");
            Console.WriteLine($"l4 and l5 are equal? = {l4.AreEqual(l5)}");
            Console.WriteLine();

            Console.WriteLine("Not equal cases...");
            Console.WriteLine($"l1 and l2 are NOT equal? = {!l1.AreEqual(l2)}");
            Console.WriteLine($"l2 and l3 are NOT equal? = {!l2.AreEqual(l3)}");
            Console.WriteLine($"l3 and l4 are NOT equal? = {!l3.AreEqual(l4)}");
            Console.WriteLine();


        }

        static public void Length_Test()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(3);
            var l3 = Helper.CreateItemHandlers(1000);
            var l4 = Helper.CreateItemHandlers(500);

            Console.WriteLine("Length test");
            Console.WriteLine($"l1 correct? = {l1.Length == 1}");
            Console.WriteLine($"l2 correct? = {l2.Length == 3}");
            Console.WriteLine($"l3 correct? = {l3.Length == 1000}");
            Console.WriteLine($"l4 correct? = {l4.Length == 500}");
            Console.WriteLine();

        }

        static public void GetLastItem_Test()
        {
            var l1 = Helper.CreateItemHandlers(1);
            var l2 = Helper.CreateItemHandlers(1000);

            var last = l1.GetLastItem();
            Console.WriteLine("Last item test...");
            Console.WriteLine($"l1's correct? {last != null && last.Next == null}");
            last = l2.GetLastItem();
            Console.WriteLine($"l2's correct? {last != null && last.Next == null}");
            Console.WriteLine();
        }

        static public void GetNthItem_Test()
        {
            var l1 = Helper.CreateItemHandlers(1);

            var item = l1.GetNthItem(1);
            Console.WriteLine("nTh item test...");
            Console.WriteLine($"l1 - correct? {item != null && item.Id == 1}");

            try
            {
                item = l1.GetNthItem(2);
                Console.WriteLine($"l1 - out of bound correct? false");
            }
            catch (Exception )
            {
                // should get here
                Console.WriteLine($"l1 - out of bound correct? true");
            }

            var l2 = Helper.CreateItemHandlers(1000);
            item = l2.GetNthItem(1000);
            Console.WriteLine($"l2's correct? {item != null && item.Id == 1000}");
            try
            {
                item = l2.GetNthItem(1001);
                Console.WriteLine($"l2 - out of bound correct? false");
            }
            catch (Exception )
            {
                // should get here
                Console.WriteLine($"l2 - out of bound correct? true");
            }


            Console.WriteLine();
        }

        #endregion

        #region loop back tests
        public static void Clone_LoopBack_List_Tests()
        {
            // 1) single item loops back to itself
            Clone_LoopBack_List_Test(1, 1);

            // 2) last item loops back to the 5th item
            Clone_LoopBack_List_Test(5, 2);

            // 3) last item loops back to the 1th item
            Clone_LoopBack_List_Test(500, 1);

            // 4) last item loops back to the last item
            Clone_LoopBack_List_Test(500, 500);

            // 5) last item loops back to the 1th item (large list test)
            Clone_LoopBack_List_Test(1000000, 1);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="nItem">first item = 1</param>
        static void Clone_LoopBack_List_Test(int length, int nItem)
        {
            var item = Helper.CreateLoopBackItems(length, nItem); 

            var clonedItem = (LinkedListItem)DeepClone.Clone(item);

            var originalHandler = new LinkedListHandler(item);
            var clonedHandler = new LinkedListHandler(clonedItem);

            var isUnique = originalHandler.IsUnique(clonedHandler);
            var areEqual = originalHandler.AreEqual(clonedHandler);

            var s = $"Cloning Loopback list - (length={length}, loopback Item={nItem})...is unique? = {isUnique}, are equal? = {areEqual}";
            Console.WriteLine(s);
            Console.WriteLine();
        }
        #endregion
    }

}
