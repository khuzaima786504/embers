using Embers.Host;
using Embers.Language;

namespace Embers.Console.Injected
{
    public static class ShortGuid
    {
        public static string NewShortGuid()
        {
            var guid = Guid.NewGuid();
            return Convert.ToBase64String(guid.ToByteArray())
                .Replace("/", "_").Replace("+", "-").Substring(0, 22);
        }
    }

    [HostFunction("short_guid")]
    internal class ShortGuidFunction : HostFunction
    {
        public override object Apply(DynamicObject self, Context context, IList<object> values)
        {
            return ShortGuid.NewShortGuid();
        }
    }
}
