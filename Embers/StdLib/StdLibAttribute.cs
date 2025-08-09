namespace Embers.StdLib
{
    /// <summary>
    /// Attribute to mark a class as a standard library function and specify its registration name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class StdLibAttribute(params string[] names) : Attribute
    {
        public string[] Names { get; } = names ?? Array.Empty<string>();
    }
}
