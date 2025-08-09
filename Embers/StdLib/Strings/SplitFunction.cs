using Embers.Language;
using Embers.Exceptions;

namespace Embers.StdLib.Strings
{
    /// <summary>
    /// Splits a string into an array using the given separator.
    /// </summary>
    [StdLib("split")]
    public class SplitFunction : StdFunction
    {
        public override object Apply(DynamicObject self, Context context, IList<object> values)
        {
            if (values == null || values.Count == 0 || values[0] == null)
                throw new ArgumentError("split expects a string argument");

            var separator = values.Count > 1 ? values[1]?.ToString() ?? "" : "";

            if (values[0] is string s)
                return s.Split(new[] { separator }, StringSplitOptions.None).ToList();

            throw new TypeError("split expects a string");
        }
    }
}
