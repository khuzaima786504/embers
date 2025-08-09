using Embers.Expressions;
using Embers.Language;

namespace Embers.Functions
{
    /// <summary>
    /// DefinedFunction represents a user-defined function with a body and parameters.
    /// This function can be called with a set of values and an optional block.
    /// The existence of a block allows for more complex behavior, such as passing in a closure or additional context.
    /// </summary>
    /// <seealso cref="Embers.Functions.ICallableWithBlock" />
    public class DefinedFunction(IExpression body, IList<string> parameters, Context context) : ICallableWithBlock
    {
        private readonly IExpression body = body;
        private readonly IList<string> parameters = parameters;
        private readonly Context context = context;

        public object ApplyWithBlock(DynamicObject self, Context context, IList<object> values, IFunction? block)
        {
            Context newcontext = new(self, this.context, block);

            int k = 0;
            foreach (var parameter in parameters)
            {
                newcontext.SetLocalValue(parameter, values[k]);
                k++;
            }

            return body.Evaluate(newcontext);
        }

        public object Apply(DynamicObject self, Context context, IList<object> values)
        {
            return ApplyWithBlock(self, context, values, null);
        }

    }
}
