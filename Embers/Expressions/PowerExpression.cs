using Embers.Exceptions;

namespace Embers.Expressions
{
    /// <summary>
    /// PowerExpression represents the power operation (exponentiation) between two expressions.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BinaryExpression" />
    public class PowerExpression(IExpression left, IExpression right)
        : BinaryExpression(left, right)
    {
        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is int li && rightvalue is int ri)
                return (int)Math.Pow(li, ri);

            if (leftvalue is double ld && rightvalue is double rd)
                return Math.Pow(ld, rd);

            throw new TypeError("Power operator requires numeric operands");
        }
    }
}
