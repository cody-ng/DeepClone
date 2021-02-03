using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone
{
    static class DeepClone
    {
        static public Object Clone(object input)
        {
            if (input == null)
                return null;

            var inputType = input.GetType();
            Console.WriteLine($"input={input}, Type={inputType}");

            // if value type, return 
            if (inputType.IsValueType)
            {
                return CloneValueType(input);
            }


            return CloneReferenceType(input, inputType);
        }

        #region helpers
        static Object CloneReferenceType(object input, Type inputType)
        {
            Console.WriteLine($"Cloning reference type {inputType.Name}...");

            if (inputType == typeof(string))
            {
                return CloneString(input);
            }

            return input;
        }

        static String CloneString(object obj)
        {
            return new string((string)obj);
        }

        static Object CloneValueType(object obj)
        {
            Console.WriteLine("Cloning value type...");
            //var temp = Convert.ChangeType(input, inputType);
            return obj;
        }
        #endregion


    }
}
