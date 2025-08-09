﻿namespace Embers.Language
{
    /// <summary>
    /// fixnumclass is a native class that represents the Fixnum type
    /// </summary>
    /// <seealso cref="Embers.Language.NativeClass" />
    public class FixnumClass(Machine machine) : NativeClass("Fixnum", machine)
    {
    }
}
