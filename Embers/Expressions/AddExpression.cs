namespace Embers.Expressions
{
    /// <summary>
    /// AddExpression represents an addition operation between two expressions.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BinaryExpression" />
    public class AddExpression(IExpression left, IExpression right) : BinaryExpression(left, right)
    {
        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is string || rightvalue is string)
                return leftvalue.ToString() + rightvalue.ToString();

            if (leftvalue is int)
                if (rightvalue is int)
                    return (int)leftvalue + (int)rightvalue;
                else
                    return (int)leftvalue + (double)rightvalue;
            else if (rightvalue is int)
                return (double)leftvalue + (int)rightvalue;
            else
                return (double)leftvalue + (double)rightvalue;
        }
    }
}
