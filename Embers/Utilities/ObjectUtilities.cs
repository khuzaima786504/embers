﻿using System.Collections;
using Embers.Functions;
using Embers.Language;

namespace Embers.Utilities
{
    /// <summary>
    /// ObjectUtilities provides methods for manipulating objects, including getting and setting values,
    /// It is used to access properties and fields dynamically, handle indexed values,bind event handlers, and check types.
    /// Objectutilities is essential for dynamic object manipulation in Embers, and adds the bridge between Embers and .NET types, allowing for dynamic access to properties, methods, and events.
    /// </summary>
    public class ObjectUtilities
    {
        public static void SetValue(object obj, string name, object value)
        {
            Type type = obj.GetType();

            type.InvokeMember(name, System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, obj, [value]);
        }

        public static object GetValue(object obj, string name)
        {
            Type type = obj.GetType();

            try
            {
                return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | /* System.Reflection.BindingFlags.InvokeMethod | */ System.Reflection.BindingFlags.Instance, null, obj, null);
            }
            catch
            {
                //return type.GetMethod(name);

                // New: Try custom GetMethod(string) on the object itself
                var getMethod = type.GetMethod("GetMethod", [typeof(string)]);
                if (getMethod != null)
                {
                    var method = getMethod.Invoke(obj, [name]);
                    if (method != null)
                        return method;
                }

                return null;
            }
        }

        public static object GetValue(object obj, string name, IList<object> arguments)
        {
            return GetNativeValue(obj, name, arguments);
        }

        public static IList<string> GetNames(object obj)
        {
            return TypeUtilities.GetNames(obj.GetType());
        }

        public static object GetNativeValue(object obj, string name, IList<object> arguments)
        {
            Type type = obj.GetType();

            try
            {
                return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance, null, obj, arguments?.ToArray());
            }
            catch
            {
                var getMethod = type.GetMethod("GetMethod", [typeof(string)]);
                if (getMethod != null)
                {
                    var method = getMethod.Invoke(obj, [name]);
                    if (method != null)
                        return method;
                }

                return null;
            }
            
        }

        public static bool IsNumber(object obj)
        {
            return obj is int ||
                obj is short ||
                obj is long ||
                obj is decimal ||
                obj is double ||
                obj is float ||
                obj is byte;
        }

        // TODO implement a method with only one index
        public static object GetIndexedValue(object obj, object[] indexes)
        {
            if (obj is Array)
                return GetIndexedValue((Array)obj, indexes);

            if (obj is IList)
                return GetIndexedValue((IList)obj, indexes);

            if (obj is IDictionary)
                return GetIndexedValue((IDictionary)obj, indexes);

            if (obj is DynamicObject && indexes != null && indexes.Length == 1)
                return ((DynamicObject)obj).GetValue((string)indexes[0]);

            return GetValue(obj, string.Empty, indexes); 
        }

        // TODO implement a method with only one index
        public static void SetIndexedValue(object obj, object[] indexes, object value)
        {
            if (obj is Array)
            {
                SetIndexedValue((Array)obj, indexes, value);
                return;
            }

            if (obj is IList)
            {
                if (indexes.Length != 1)
                    throw new InvalidOperationException("Invalid number of subindices");

                int index = (int)indexes[0];

                IList list = (IList)obj;

                if (list.Count == index)
                    list.Add(value);
                else
                    list[index] = value;

                return;
            }

            if (obj is IDictionary)
            {
                if (indexes.Length != 1)
                    throw new InvalidOperationException("Invalid number of subindices");

                ((IDictionary)obj)[indexes[0]] = value;

                return;
            }

            // TODO as in GetIndexedValue, consider Default member
            throw new InvalidOperationException(string.Format("Not indexed value of type {0}", obj.GetType().ToString()));
        }

        public static void SetIndexedValue(Array array, object[] indexes, object value)
        {
            switch (indexes.Length)
            {
                case 1:
                    array.SetValue(value, (int)indexes[0]);
                    return;
                case 2:
                    array.SetValue(value, (int)indexes[0], (int)indexes[1]);
                    return;
                case 3:
                    array.SetValue(value, (int)indexes[0], (int)indexes[1], (int)indexes[2]);
                    return;
            }

            throw new InvalidOperationException("Invalid number of subindices");
        }

        public static void AddHandler(object obj, string eventname, IFunction function, Context context)
        {
            var type = obj.GetType();
            var @event = type.GetEvent(eventname);
            var invoke = @event.EventHandlerType.GetMethod("Invoke");
            var parameters = invoke.GetParameters();
            int npars = parameters.Count();
            _ = new Type[npars + 1];
            Type wrappertype = null;
            Type[] partypes = new Type[npars + 2];
            Type rettype = invoke.ReturnParameter.ParameterType;
            bool isaction = rettype.FullName == "System.Void";

            if (isaction)
                rettype = typeof(int);

            switch (npars)
            {
                case 0:
                    partypes[0] = rettype;
                    partypes[1] = @event.EventHandlerType;
                    wrappertype = typeof(FunctionWrapper<,>).MakeGenericType(partypes);
                    break;
                case 1:
                    partypes[0] = parameters.ElementAt(0).ParameterType;
                    partypes[1] = rettype;
                    partypes[2] = @event.EventHandlerType;
                    wrappertype = typeof(FunctionWrapper<,,>).MakeGenericType(partypes);
                    break;
                case 2:
                    partypes[0] = parameters.ElementAt(0).ParameterType;
                    partypes[1] = parameters.ElementAt(1).ParameterType;
                    partypes[2] = rettype;
                    partypes[3] = @event.EventHandlerType;
                    wrappertype = typeof(FunctionWrapper<,,,>).MakeGenericType(partypes);
                    break;
                case 3:
                    partypes[0] = parameters.ElementAt(0).ParameterType;
                    partypes[1] = parameters.ElementAt(1).ParameterType;
                    partypes[2] = parameters.ElementAt(2).ParameterType;
                    partypes[3] = rettype;
                    partypes[4] = @event.EventHandlerType;
                    wrappertype = typeof(FunctionWrapper<,,,,>).MakeGenericType(partypes);
                    break;
            }

            object wrapper = Activator.CreateInstance(wrappertype, function, context);

            @event.AddEventHandler(obj, (Delegate)GetValue(wrapper, isaction ? "CreateActionDelegate" : "CreateFunctionDelegate", null));
        }

        private static object GetIndexedValue(Array array, object[] indexes)
        {
            switch (indexes.Length)
            {
                case 1:
                    return array.GetValue((int)indexes[0]);
                case 2:
                    return array.GetValue((int)indexes[0], (int)indexes[1]);
                case 3:
                    return array.GetValue((int)indexes[0], (int)indexes[1], (int)indexes[2]);
            }

            throw new InvalidOperationException("Invalid number of subindices");
        }

        private static object GetIndexedValue(IList list, object[] indexes)
        {
            if (indexes.Length != 1)
                throw new InvalidOperationException("Invalid number of subindices");

            return list[(int)indexes[0]];
        }

        private static object GetIndexedValue(IDictionary dictionary, object[] indexes)
        {
            if (indexes.Length != 1)
                throw new InvalidOperationException("Invalid number of subindices");

            return dictionary[indexes[0]];
        }
    }
}
