using System.Collections;

namespace Embers.Language
{
    /// <summary>
    /// NativeClass resolves Ember runtime types to C# equivalents.
    /// </summary>
    /// <seealso cref="Embers.Language.DynamicObject" />
    public class NativeClass : DynamicObject
    {
        private readonly string name;
        private readonly Machine machine;
        private readonly IDictionary<string, Func<object, IList<object>, object>> methods = new Dictionary<string, Func<object, IList<object>, object>>();

        private NativeClass fixnumclass;
        private NativeClass floatclass;
        private NativeClass stringclass;
        private NativeClass nilclass;
        private NativeClass falseclass;
        private NativeClass trueclass;
        private NativeClass arrayclass;
        private NativeClass hashclass;
        private NativeClass rangeclass;

        public NativeClass(string name, Machine machine)
            : base(null)
        {
            this.name = name;
            this.machine = machine;
            SetInstanceMethod("class", MethodClass);
        }

        public string Name { get { return name; } }

        public void SetInstanceMethod(string name, Func<object, IList<object>, object> method)
        {
            methods[name] = method;
        }

        public Func<object, IList<object>, object>? GetInstanceMethod(string name)
        {
            if (methods.TryGetValue(name, out Func<object, IList<object>, object>? value))
                return value;

            return null;
        }

        public override string ToString()
        {            
            return Name;
        }

        public object? MethodClass(object self, IList<object> values)
        {
            if (self == null)
            {
                nilclass ??= (NativeClass)machine.RootContext.GetLocalValue("NilClass");

                return nilclass;
            }

            if (self is int)
            {
                fixnumclass ??= (NativeClass)machine.RootContext.GetLocalValue("Fixnum");

                return fixnumclass;
            }

            if (self is double)
            {
                floatclass ??= (NativeClass)machine.RootContext.GetLocalValue("Float");

                return floatclass;
            }

            if (self is string)
            {
                stringclass ??= (NativeClass)machine.RootContext.GetLocalValue("String");

                return stringclass;
            }

            if (self is bool)
                if ((bool)self)
                {
                    trueclass ??= (NativeClass)machine.RootContext.GetLocalValue("TrueClass");

                    return trueclass;
                }
                else
                {
                    falseclass ??= (NativeClass)machine.RootContext.GetLocalValue("FalseClass");

                    return falseclass;
                }

            if (self is IDictionary)
            {
                hashclass ??= (NativeClass)machine.RootContext.GetLocalValue("Hash");

                return hashclass;
            }

            if (self is IList)
            {
                arrayclass ??= (NativeClass)machine.RootContext.GetLocalValue("Array");

                return arrayclass;
            }

            if (self is IEnumerable)
            {
                rangeclass ??= (NativeClass)machine.RootContext.GetLocalValue("Range");

                return rangeclass;
            }

            return null;
        }
    }
}
