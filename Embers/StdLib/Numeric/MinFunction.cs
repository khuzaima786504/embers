using Embers.Language;
using Embers.Exceptions;

namespace Embers.StdLib.Numeric
{
    /// <summary>
    /// Returns the minimum value from a list of numbers.
    /// </summary>
    [StdLib("min")]
    public class MinFunction : StdFunction
    {
        public override object Apply(DynamicObject self, Context context, IList<object> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentError("min expects at least one argument");

            try
            {
                return values.Min(v => Convert.ToDouble(v));
            }
            catch
            {
                throw new TypeError("min expects numeric arguments");
            }
        }
    }
}
