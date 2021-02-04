using DeepClone.Model.ReferenceType;
using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static public class ReferenceTypeTest
    {
        static public Object CreateBaseTestClass()
        {
            return new BaseTestClass() { Id=1, Name="test", Count=10};
        }

        static public Object CreateDerivedTestClass()
        {
            return new DerivedTestClass()
            {
                Id = 1,
                Name = "test",
                Count = 10,
                Derived_Id = 10,
                Derived_Name = "derived",
                Derived_Count = 20
            };
        }

        static public Object CreateString()
        {
            return "this is a string";
        }
    }

}
