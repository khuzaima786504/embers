using Embers.Language;

namespace Embers.StdLib
{
    [StdLib("puts", "p", "print")]
    public class PutsFunction(TextWriter writer) : StdFunction
    {
        private readonly TextWriter writer = writer;

        public PutsFunction() : this(Console.Out) { }

        public override object? Apply(DynamicObject self, Context context, IList<object> values)
        {
            foreach (var value in values)
                writer.WriteLine(value);

            return null;
        }
    }
}
