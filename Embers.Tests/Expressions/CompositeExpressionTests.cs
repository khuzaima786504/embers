using Embers.Expressions;

namespace Embers.Tests.Expressions
{
    [TestClass]
    public class CompositeExpressionTests
    {
        [TestMethod]
        public void ExecuteTwoAssignCommands()
        {
            Context context = new();
            AssignExpression expr1 = new("one", new ConstantExpression(1));
            AssignExpression expr2 = new("two", new ConstantExpression(2));
            CompositeExpression expr = new([expr1, expr2]);

            var result = expr.Evaluate(context);

            Assert.AreEqual(2, result);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            Context context = new();
            AssignExpression expr1 = new("one", new ConstantExpression(1));
            AssignExpression expr2 = new("two", new ConstantExpression(2));
            CompositeExpression expr = new([expr1, expr2]);

            var result = expr.GetLocalVariables();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains("one"));
            Assert.IsTrue(result.Contains("two"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignExpression aexpr1 = new("one", new ConstantExpression(1));
            AssignExpression aexpr2 = new("two", new ConstantExpression(2));

            CompositeExpression expr1 = new([aexpr1, aexpr2]);
            CompositeExpression expr2 = new([aexpr2, aexpr1]);
            CompositeExpression expr3 = new([aexpr1]);
            CompositeExpression expr4 = new([aexpr1, aexpr2]);

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }
    }
}
