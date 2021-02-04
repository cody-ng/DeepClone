using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static public class LinkedListTest
    {

        static public LinkedListItem CreateItems(int count)
        {
            if (count < 1)
                return null;

            var head = CreateItem(1, "Head");
            var temp = head;

            for (int i=1;i< count; i++)
            {
                temp.Next = CreateItem(i, $"Item - {i}");
                temp = temp.Next;
            }

            return head;

        }

        // compare 2 list items to ensure every item is unique
        // (there are no shared items between the 2 lists)
        public static bool IsUnique(LinkedListItem list1, LinkedListItem list2)
        {
            // 1) put list1's items into hash
            var hash = new HashSet<LinkedListItem>();
            var temp = list1;

            while (temp != null)
            {
                hash.Add(temp);
                temp = temp.Next;
            }

            // 2) make sure no items in list2 appears in hash
            temp = list2;
            while (temp != null)
            {
                if (hash.Contains(temp))
                    return false;

                temp = temp.Next;
            }

            return true;
        }

        static LinkedListItem CreateItem(int id, string baseName)
        {
            return new LinkedListItem()
            {
                Id = id,
                Name = baseName
            };
        }

    }

}
