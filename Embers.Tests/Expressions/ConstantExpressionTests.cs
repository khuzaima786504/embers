using Embers.Expressions;

namespace Embers.Tests.Expressions
{
    [TestClass]
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void EvaluateAsInteger()
        {
            ConstantExpression expr = new(123);

            Assert.AreEqual(123, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            ConstantExpression expr1 = new(123);
            ConstantExpression expr2 = new(124);
            ConstantExpression expr3 = new(123);

            Assert.IsTrue(expr1.Equals(expr1));
            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));

            Assert.IsFalse(expr1.Equals(null));
        }

        [TestMethod]
        public void EqualsWhenValueIsNull()
        {
            ConstantExpression expr1 = new(null);
            ConstantExpression expr2 = new(null);
            ConstantExpression expr3 = new("foo");

            Assert.IsTrue(expr1.Equals(expr1));
            Assert.IsTrue(expr1.Equals(expr2));
            Assert.IsTrue(expr2.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr2.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }
    }
}
