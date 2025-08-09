﻿using Embers.Exceptions;
using Embers.Language;

namespace Embers.Expressions
{
    /// <summary>
    /// AliasExpression is used to create an alias for a method in a class.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BaseExpression" />
    public class AliasExpression(string newName, string oldName) : BaseExpression
    {
        private readonly string newName = newName;
        private readonly string oldName = oldName;

        public override object? Evaluate(Context context)
        {
            if (context.Self.Class is not DynamicClass dclass)
                throw new TypeError("alias must be used inside a class definition");

            dclass.AliasMethod(newName, oldName);
            return null;
        }

        public override bool Equals(object? obj)
        {
            return obj is AliasExpression other &&
                   newName == other.newName &&
                   oldName == other.oldName;
        }

        public override int GetHashCode()
        {
            return newName.GetHashCode() ^ oldName.GetHashCode();
        }
    }
}
