﻿namespace Embers.Expressions
{
    /// <summary>
    /// ClassVarExpression represents a class variable expression.
    /// A class variable is a variable that is shared across all instances of a class.
    /// For instance variable expressions, use <seealso cref="Embers.Expressions.InstanceVarExpression" />
    /// </summary>
    /// <seealso cref="Embers.Expressions.BaseExpression" />
    public class ClassVarExpression(string name) : BaseExpression
    {
        private static readonly int hashcode = typeof(ClassVarExpression).GetHashCode();
        private readonly string name = name;

        public string Name { get { return name; } }

        public override object Evaluate(Context context)
        {
            var result = context.Self.Class.GetValue(name);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassVarExpression) 
            {
                var expr = (ClassVarExpression)obj;

                return Name.Equals(expr.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + hashcode;
        }
    }
}
