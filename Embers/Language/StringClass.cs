namespace Embers.Language
{
    /// <summary>
    /// StringClass represents the native String class in the interpreter.
    /// </summary>
    /// <seealso cref="Embers.Language.NativeClass" />
    public class StringClass(Machine machine) : NativeClass("String", machine)
    {
    }
}
