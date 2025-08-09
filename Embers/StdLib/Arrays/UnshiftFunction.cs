using Embers.Language;
using Embers.Exceptions;

namespace Embers.StdLib.Arrays
{
    [StdLib("unshift")]
    public class UnshiftFunction : StdFunction
    {
        public override object Apply(DynamicObject self, Context context, IList<object> values)
        {
            if (values == null || values.Count < 2 || values[0] == null)
                throw new ArgumentError("unshift expects an array and a value");

            if (values[0] is IList<object> arr)
            {
                arr.Insert(0, values[1]);
                return arr;
            }

            throw new TypeError("unshift expects an array as first argument");
        }
    }
}
