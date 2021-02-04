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

    class DerivedTestClass : BaseTestClass
    {
        public long Derived_Id;
        public int Derived_Count;
        public string Derived_Name;

        public override string ToString()
        {
            var s = base.ToString() + 
                    Environment.NewLine + 
                    $"Derived_Id={Derived_Id}, Derived_Count={Derived_Count}, Derived_Name={Derived_Name}";

            return s;
        }
    }

}
