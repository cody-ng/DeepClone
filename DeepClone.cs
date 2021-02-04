using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepClone
{
    static class DeepClone
    {
        /// <summary>
        /// Clones an object for the following fields:
        /// - public fields only
        ///  
        /// Does not include the following:
        /// - static fields
        /// - collection fields (array, list, ...etc)
        /// - properties
        /// - private or protected fields
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public Object Clone(object input)
        {
            if (input == null)
                return null;

            var inputType = input.GetType();
            Console.WriteLine($"input={input}, Type={inputType}");

            // if value type, return 
            if (inputType.IsValueType)
            {
                return CloneValueType(input, inputType);
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

            var newObj = Activator.CreateInstance(inputType);
            

            var fieldInfos = inputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach(var f in fieldInfos)
            {
                var ft = f.FieldType;
                var fvalue = f.GetValue(input);

                //Console.WriteLine($"Name = {f.Name}, Value = {fvalue}, FieldType = {ft}, ValueType = {ft.IsValueType}");
                if (ft.IsValueType)
                {
                    f.SetValue(newObj, fvalue);
                }
                else
                {
                    var refTypeObj = CloneReferenceType(fvalue, ft);
                    f.SetValue(newObj, refTypeObj);
                }
            }

            return newObj;
        }

        static String CloneString(object obj)
        {
            return new string((string)obj);
        }

        static Object CloneValueType(object input, Type inputType)
        {
            Console.WriteLine("Cloning value type...");
            return Convert.ChangeType(input, inputType);
            //return input;
        }
        #endregion


    }
}
