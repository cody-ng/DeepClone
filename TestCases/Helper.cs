using DeepClone.Model.ReferenceType;
using DeepClone.ReferenceType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static class Helper
    {

        static public LinkedListItem CreateItems(int count)
        {
            if (count < 1)
                return null;

            var head = CreateItem(1, "Head");
            var temp = head;

            for (int i = 1; i < count; i++)
            {
                temp.Next = CreateItem(i, $"Item - {i}");
                temp = temp.Next;
            }

            return head;

        }
        static LinkedListItem CreateItem(int id, string baseName)
        {
            return new LinkedListItem()
            {
                Id = id,
                Name = baseName
            };
        }

        static public LinkedListHandler CreateItemHandlers(int count)
        {
            return new LinkedListHandler(CreateItems(count));
        }

    }
}
