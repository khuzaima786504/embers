using Embers.Exceptions;

namespace Embers.Expressions
{
    /// <summary>
    /// ModuloExpression represents the modulo operation between two expressions.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BinaryExpression" />
    public class ModuloExpression(IExpression left, IExpression right)
        : BinaryExpression(left, right)
    {
        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is int li && rightvalue is int ri)
                return li % ri;

            throw new TypeError("Modulo operator requires integer operands");
        }
    }
}
