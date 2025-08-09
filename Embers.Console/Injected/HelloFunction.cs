using Embers.Host;
using Embers.Language;

namespace Embers.Console.Injected;

[HostFunction("hello")]
internal class HelloFunction : HostFunction
{
    public override object Apply(DynamicObject self, Context context, IList<object> values)
    {
        System.Console.WriteLine("Hello from Embers.Console!");
        return null;
    }
}
