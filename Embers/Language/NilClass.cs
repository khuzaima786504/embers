namespace Embers.Language
{
    /// <summary>
    /// NilClass represents Null
    /// </summary>
    /// <seealso cref="Embers.Language.NativeClass" />
    public class NilClass(Machine machine) : NativeClass("NilClass", machine)
    {
    }
}
