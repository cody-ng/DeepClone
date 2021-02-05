using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepClone
{
    class DeepClone
    {
        #region class fields
        protected Dictionary<object, object> CachedObjects { get; set; } = new Dictionary<object, object>();
        #endregion

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
            //Console.WriteLine($"DeepClone.Clone() = input={input}, Type={inputType}");

            // if primitve value type, return 
            if (inputType.IsValueType && inputType.IsPrimitive)
            {
                return CloneValueType(input, inputType);
            }

            var deepcloner = new DeepClone();

            return deepcloner.CloneReferenceType(input, inputType);
        }

        #region helpers

        static Object CloneValueType(object input, Type inputType)
        {
            Console.WriteLine("Cloning value type...");
            return Convert.ChangeType(input, inputType);
            //return input;
        }

        Object CloneReferenceType(object input, Type inputType)
        {
            //Console.WriteLine($"Cloning reference type {inputType.Name}...");

            if (input == null)
                return null;

            // special case for string (primitive)
            if (inputType == typeof(string))
            {
                return CloneString(input);
            }

            // detect cyclic loop by checking if the input has previously been traversed
            object cachedObj = null;
            if( this.CachedObjects.TryGetValue(input, out cachedObj) )
            {
                // loop found, no need to clone input
                return cachedObj;
            }

            // create a new object of the same reference type as the input
            var newObj = Activator.CreateInstance(inputType);

            // cache the new object
            this.CachedObjects.Add(input, newObj);


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

        String CloneString(object obj)
        {
            return new string((string)obj);
        }

        #endregion


    }
}
