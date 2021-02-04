using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.ReferenceType
{
    /// <summary>
    /// Handles the LinkedListItem item as a list for testing purpose.
    /// 
    /// ***Do not modify the underlying LinkedListItem items after contructor. ***
    /// </summary>
    public class LinkedListHandler
    {
        public LinkedListHandler(LinkedListItem item)
        {
            this.Init(item);
        }

        #region fields and properties
        protected LinkedListItem Item { get; set; }
        public int Length { get; set; }


        protected HashSet<LinkedListItem> ItemHash {get;set;} = new HashSet<LinkedListItem>();
        #endregion

        protected void Clear()
        {
            this.Item = null;
            this.Length = 0;
            this.ItemHash.Clear();
        }

        protected void Init(LinkedListItem item)
        {
            Clear();

            this.Item = item;

            int length = 0;
            var temp = this.Item;

            // loop thru the list to:
            // 1) put items in hash
            // 2) compute length 
            while (temp != null)
            {
                // this.check for cyclic loop.  Once loop is detected, break
                if (this.ItemHash.Contains(temp))
                    break;
                this.ItemHash.Add(temp);

                ++length;
                temp = temp.Next;
            }

            this.Length = length;
        }



        // compare 2 list items to ensure every item is unique
        // (there are no shared items between the 2 lists)
        public bool IsUnique(LinkedListHandler otherItem)
        {
            if (object.ReferenceEquals(this, otherItem))
                return false;

            foreach(var i in this.ItemHash)
            {
                if (otherItem.ItemHash.Contains(i))
                    return false;
            }

            return true; // no duplicates => unique
        }

        // Make sure the 2 lists have equal value
        // (*** does not check for unique items.  Use IsUnique() *** )
        public bool AreEqual(LinkedListHandler otherItem)
        {
            if (this.Length != otherItem.Length)
                return false;

            // Starting from head of the list to the last item,
            // check whether both items' value are equal.
            var item1 = this.Item;
            var item2 = otherItem.Item;

            while( item1 != null)
            {
                if (!item1.Equals(item2))
                    return false;

                item1 = item1.Next;
                item2 = item2.Next;
            }

            return true; // all equal
        }

    }

}
