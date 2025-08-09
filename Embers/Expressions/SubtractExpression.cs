namespace Embers.Expressions
{
    /// <summary>
    /// SubtractExpression represents a binary expression that performs subtraction between two expressions.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BinaryExpression" />
    public class SubtractExpression(IExpression left, IExpression right) : BinaryExpression(left, right)
    {
        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is int)
                if (rightvalue is int)
                    return (int)leftvalue - (int)rightvalue;
                else
                    return (int)leftvalue - (double)rightvalue;
            else if (rightvalue is int)
                return (double)leftvalue - (int)rightvalue;
            else
                return (double)leftvalue - (double)rightvalue;
        }
    }
}
