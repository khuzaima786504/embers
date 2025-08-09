namespace Embers.Language
{
    /// <summary>
    /// HashClass represents the native Hash class in the runtime interpreter.
    /// </summary>
    /// <seealso cref="Embers.Language.NativeClass" />
    public class HashClass(Machine machine) : NativeClass("Hash", machine)
    {
    }
}
