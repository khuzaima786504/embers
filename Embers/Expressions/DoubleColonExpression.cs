using Embers.Exceptions;
using Embers.Language;
using Embers.Utilities;

namespace Embers.Expressions
{
    /// <summary>
    /// DoubleColonExpression represents a double colon (::) expression.
    /// This expression is used to access constants or static members of a class or module.
    /// </summary>
    /// <seealso cref="Embers.Expressions.BaseExpression" />
    /// <seealso cref="Embers.Expressions.INamedExpression" />
    public class DoubleColonExpression(IExpression expression, string name) : BaseExpression, INamedExpression
    {
        private static readonly int hashcode = typeof(DoubleColonExpression).GetHashCode();

        private readonly IExpression expression = expression;
        private readonly string name = name;

        public IExpression TargetExpression { get { return expression; } }

        public string Name { get { return name; } }

        public override object Evaluate(Context context)
        {
            //_ = [];
            var result = expression.Evaluate(context);

            if (result is Type)
                return TypeUtilities.ParseEnumValue((Type)result, name);

            var obj = (DynamicClass)result;

            if (!obj.Constants.HasLocalValue(name))
                throw new NameError(string.Format("unitialized constant {0}::{1}", obj.Name, name));

            return obj.Constants.GetLocalValue(name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DoubleColonExpression)
            {
                var expr = (DoubleColonExpression)obj;

                return name.Equals(expr.name) && expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = name.GetHashCode() + expression.GetHashCode() + hashcode;

            return result;
        }
    }
}
