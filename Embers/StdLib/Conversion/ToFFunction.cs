using Embers.Language;
using Embers.Exceptions;

namespace Embers.StdLib.Conversion
{
    /// <summary>
    /// Converts the value to a floating point number.
    /// </summary>
    [StdLib("to_f", "to_float")]
    public class ToFFunction : StdFunction
    {
        public override object Apply(DynamicObject self, Context context, IList<object> values)
        {
            if (values == null || values.Count == 0 || values[0] == null)
                return 0.0;

            var value = values[0];
            try
            {
                if (value is double d) return d;
                if (value is int i) return (double)i;
                if (value is string s) return double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
                return Convert.ToDouble(value);
            }
            catch
            {
                throw new TypeError("to_f expects a numeric or string value");
            }
        }
    }
}
