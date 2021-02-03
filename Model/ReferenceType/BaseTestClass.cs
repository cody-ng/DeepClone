using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Model.ReferenceType
{
    class BaseTestClass
    {
        public long Id;
        public int Count;
        public string Name;

        public override string ToString()
        {
            return $"Id={Id}, Count={Count}, Name={Name}";
        }

    }
}
