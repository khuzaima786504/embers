using Embers.StdLib.Arrays;
using System.Collections;

namespace Embers.Language
{
    public class DynamicArray : ArrayList
    {
        public override object this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    return null;

                return base[index];
            }

            set
            {
                while (index >= Count)
                    Add(null);

                base[index] = value;
            }
        }

        public override string ToString()
        {
            var result = "[";

            foreach (var value in this)
            {
                if (result.Length > 1)
                    result += ", ";

                if (value == null)
                    result += "nil";
                else
                    result += value.ToString();
            }

            result += "]";

            return result;
        }

        public object GetMethod(string name)
        {
            return name switch
            {
                "map" => new MapFunction(),
                // Add others as needed, e.g.:
                // "compact" => new CompactFunction(),
                _ => null
            };
        }
    }
}
