﻿namespace Embers.Language
{
    /// <summary>
    /// Symbols are used to represent identifiers in the language.
    /// A symbol is a unique identifier that is immutable and interned.
    /// </summary>
    public class Symbol(string name)
    {
        private static readonly int hashcode = typeof(Symbol).GetHashCode();
        private readonly string name = name;

        public override string ToString()
        {
            return ":" + name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Symbol)
            {
                var symbol = (Symbol)obj;

                return name == symbol.name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + hashcode;
        }
    }
}
