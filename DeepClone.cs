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


        #region inner class
        class ReferencedTypeStackItem
        {
            public ReferencedTypeStackItem(Object headInput, Type headInputType)
            {
                this.Input = headInput;
                this.InputType = headInputType;
                this.IsHead = true;
            }

            public ReferencedTypeStackItem(Object fieldInput, Type fieldInputType, Object newObject, FieldInfo field)
            {
                this.Input = fieldInput;
                this.InputType = fieldInputType;
                this.Parent = newObject;
                this.Field = field;
            }


            public Object Input { get; protected set; }
            public Type InputType { get; protected set; }
            /// <summary>
            /// If exists, then the "input" is a field in this object.
            /// If null, it's the head of the list.
            /// </summary>
            public Object Parent { get; protected set; }
            public bool IsHead { get; protected set; }

            private FieldInfo _Field; // backing field
            public FieldInfo Field 
            {
                get
                {
                    if (_Field == null)
                        throw new ApplicationException("Field property is null");
                    return _Field;
                }
                protected set 
                {
                    _Field = value;
                }
            }

            /// <summary>
            /// Is the object input a field of another object?
            /// </summary>
            /// <returns></returns>
            public bool HasParent
            {
                get
                {
                    return this.Parent != null;
                }
            }
        }
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

            return deepcloner.CloneReferenceType_Iterative(input, inputType);

            // *** do not use ***
            //the recursive implementation throws stackoverflow exception when the list item counts are > 54000 or so
            //return deepcloner.CloneReferenceType(input, inputType);
        }

        #region helpers

        static Object CloneValueType(object input, Type inputType)
        {
            Console.WriteLine("Cloning value type...");
            return Convert.ChangeType(input, inputType);
            //return input;
        }

        /// <summary>
        /// Recursive method is not used.  
        /// StackOverFlow exception when creating about 5400 list items
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
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


        Object CloneReferenceType_Iterative(object headItem, Type itemType)
        {
            //Console.WriteLine($"Cloning reference type {inputType.Name}...");
            var stack = new Stack<ReferencedTypeStackItem>();
            stack.Push(new ReferencedTypeStackItem(headItem, itemType));

            Object result = null;

            while ( stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Input == null)
                {
                    // should not get here
                    throw new ApplicationException("why current.Input == null ???");
                }
                // special case for string (primitive)
                if (current.InputType == typeof(string))
                {
                    if( current.HasParent )
                    {
                        var refTypeObj = CloneString(current.Input);
                        current.Field.SetValue(current.Parent, refTypeObj);
                    }
                    else
                    {
                        result = CloneString(current.Input);
                    }
                    continue;
                }

                // detect cyclic loop by checking if the input has previously been traversed
                object cachedObj = null;
                if (this.CachedObjects.TryGetValue(current.Input, out cachedObj))
                {
                    // loop found, no need to clone input
                    if (current.HasParent )
                    {
                        current.Field.SetValue(current.Parent, cachedObj);
                    }
                    continue;
                }
                else
                {
                    // create a new object of the same reference type as the input
                    var newObj = Activator.CreateInstance(current.InputType);

                    if( current.IsHead)
                    {
                        // only return the head of the list
                        result = newObj;
                    }

                    // cache the new object
                    this.CachedObjects.Add(current.Input, newObj);

                    // set the new cloned object to the parent's field
                    if (current.HasParent)
                    {
                        current.Field.SetValue(current.Parent, newObj);
                    }

                    var fieldInfos = current.InputType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var f in fieldInfos)
                    {
                        var ft = f.FieldType;
                        var fvalue = f.GetValue(current.Input);

                        //Console.WriteLine($"Name = {f.Name}, Value = {fvalue}, FieldType = {ft}, ValueType = {ft.IsValueType}");
                        if (ft.IsValueType)
                        {
                            f.SetValue(newObj, fvalue);
                        }
                        else if (fvalue == null)
                        {
                            f.SetValue(newObj, fvalue);
                        }
                        else
                        {
                            stack.Push(new ReferencedTypeStackItem(fvalue, ft, newObj, f));
                        }
                    }
                }
            }

            return result;
        }

        String CloneString(object obj)
        {
            return new string((string)obj);
        }

        #endregion


    }
}
