using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Model.ReferenceType
{
 
    public class LinkedListItem : IEquatable<LinkedListItem>
    {
        public long Id;
        public string Name;

        public LinkedListItem Next;

        bool IsLast()
        {
            return this.Next == null;
        }


        public bool Equals(LinkedListItem obj)
        {
            // value comparison only
            if (this.Id == obj.Id)
            {
                if (this.Name == null && obj.Name == null
                    || this.Name == string.Empty && obj.Name == string.Empty
                    || string.Compare(this.Name, obj.Name) == 0
                    )
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"(Id={Id}, Name={Name})";
        }

    }

}
