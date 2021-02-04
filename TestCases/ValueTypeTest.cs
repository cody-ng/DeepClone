using DeepClone.Model.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.TestCases
{
    static public class ValueTypeTest
    {
        static public Object CreateInteger()
        {
            return 1;
        }

        static public Object CreateBook()
        {
            return new Book()
            {
                Id = 1,
                IsColor = true,
                NumPages = 100
            };
        }

    }

}
