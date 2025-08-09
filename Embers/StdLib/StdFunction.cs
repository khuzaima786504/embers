﻿using Embers.Functions;
using Embers.Language;

namespace Embers.StdLib
{
    /// <summary>
    /// Standard function base class for all functions in the standard library.
    /// Implements from <see cref="IFunction"/> and provides a common interface for applying functions.
    /// Clases derived from StdFunction will automatically be included in the standard library and can be used as functions in the language.
    /// </summary>
    /// <seealso cref="IFunction" />
    public abstract class StdFunction : IFunction
    {
        public abstract object Apply(DynamicObject self, Context context, IList<object> values);

        public virtual object ApplyWithBlock(DynamicObject self, Context context, IList<object> values, IFunction block)
        {
            var previous = context.Block;

            try
            {
                // Assign the block temporarily
                typeof(Context)
                    .GetField("<Block>k__BackingField", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?
                    .SetValue(context, block);

                return Apply(self, context, values);
            }
            finally
            {
                typeof(Context)
                    .GetField("<Block>k__BackingField", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?
                    .SetValue(context, previous);
            }
        }

    }
}
