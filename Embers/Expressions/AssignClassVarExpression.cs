﻿namespace Embers.Expressions
{
    /// <summary>
    /// AssignExpression for setting a class variable in the current context.
    /// </summary>
    /// <seealso cref="Embers.Expressions.IExpression" />
    public class AssignClassVarExpression(string name, IExpression expression) : IExpression
    {
        private static readonly int hashtag = typeof(AssignClassVarExpression).GetHashCode();

        private readonly string name = name;
        private readonly IExpression expression = expression;

        public string Name { get { return name; } }

        public IExpression Expression { get { return expression; } }

        public IList<string> GetLocalVariables()
        {
            return expression.GetLocalVariables();
        }

        public object Evaluate(Context context)
        {
            object value = expression.Evaluate(context);
            context.Self.Class.SetValue(name, value);
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignClassVarExpression)
            {
                var cmd = (AssignClassVarExpression)obj;

                return Name.Equals(cmd.name) && Expression.Equals(cmd.Expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Expression.GetHashCode() + hashtag;
        }
    }
}
