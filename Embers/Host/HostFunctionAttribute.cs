namespace Embers.Host
{
    /// <summary>
    /// Attribute to mark a class as a host function and specify its registration names.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class HostFunctionAttribute(params string[] names) : Attribute
    {
        public string[] Names { get; } = names;
    }
}
