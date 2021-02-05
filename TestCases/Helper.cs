using DeepClone.Model.ReferenceType;
using DeepClone.ReferenceType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static class Helper
    {
        /// <summary>
        /// Create a list of items where the last item loops back
        /// to one of the previous item in the list
        /// </summary>
        /// <param name="count"></param>
        /// <param name="nItemToLoopBackto">the previous nth item to be looped back to by the last item.</param>
        /// <returns></returns>
        static public LinkedListItem CreateLoopBackItems(int count, int nItemToLoopBackto)
        {
            if (count < 1)
                throw new ArgumentException("count must be greater than 0");
            if(nItemToLoopBackto > count)
                throw new ArgumentException("nItemToLoopBackto must be less than count");

            var head = CreateItems(count);

            var handler = new LinkedListHandler(head);

            var last = handler.GetLastItem();
            var nItem = handler.GetNthItem(nItemToLoopBackto);

            handler.Clear();

            // create loop back
            last.Next = nItem;

            return head;
        }


        static public LinkedListItem CreateItems(int count)
        {
            if (count < 1)
                return null;

            var head = CreateItem(1, "Head"); // first item's ID = 1
            var temp = head;

            for (int i = 1; i < count; i++)
            {
                var id = i + 1;
                temp.Next = CreateItem(id, $"Item - {id}");
                temp = temp.Next;
            }

            return head;

        }
        
        static public LinkedListHandler CreateItemHandlers(int count)
        {
            return new LinkedListHandler(CreateItems(count));
        }


        #region helper functions
        static LinkedListItem CreateItem(int id, string baseName)
        {
            return new LinkedListItem()
            {
                Id = id,
                Name = baseName
            };
        }


        #endregion

    }
}
