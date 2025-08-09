﻿using Embers.Functions;

namespace Embers.Language
{
    public class DynamicObject(DynamicClass @class)
    {
        private DynamicClass @class = @class;
        private DynamicClass singletonclass;
        private readonly IDictionary<string, object> values = new Dictionary<string, object>();

        public DynamicClass @Class { get { return @class; } }

        public DynamicClass SingletonClass
        {
            get
            {
                if (singletonclass == null)
                {
                    singletonclass = new DynamicClass(string.Format("#<Class:{0}>", ToString()), @class);
                    singletonclass.SetClass(@class.Class);
                }

                return singletonclass;
            }
        }

        public void SetValue(string name, object value)
        {
            values[name] = value;
        }

        public object? GetValue(string name)
        {
            if (values.TryGetValue(name, out object? value))
                return value;

            return null;
        }

        public virtual IFunction? GetMethod(string name)
        {
            if (singletonclass != null)
                return singletonclass.GetInstanceMethod(name);

            if (@class != null)
                return @class.GetInstanceMethod(name);

            return null;
        }

        public override string ToString()
        {
            return string.Format("#<{0}:0x{1}>", Class.Name, GetHashCode().ToString("x"));
        }

        internal void SetClass(DynamicClass @class)
        {
            this.@class = @class;
        }
    }
}
