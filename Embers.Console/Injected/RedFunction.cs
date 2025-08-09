using Crayon;
using Embers.Host;
using Embers.Language;

namespace Embers.Console.Injected;

[HostFunction("danger")]
internal class RedFunction : HostFunction
{
    public override object Apply(DynamicObject self, Context context, IList<object> values)
    {
        foreach (var value in values)
            System.Console.Write(Output.Red(value.ToString()));

        System.Console.WriteLine("");

        return null;
    }
}
