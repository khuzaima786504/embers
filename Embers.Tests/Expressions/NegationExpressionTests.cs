using Embers.Expressions;

namespace Embers.Tests.Expressions
{
    [TestClass]
    public class NegationExpressionTests
    {
        [TestMethod]
        public void NegationFalse()
        {
            NegationExpression expr = new(new ConstantExpression(false));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationTrue()
        {
            NegationExpression expr = new(new ConstantExpression(true));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationEmptyString()
        {
            NegationExpression expr = new(new ConstantExpression(string.Empty));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationString()
        {
            NegationExpression expr = new(new ConstantExpression("foo"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationInteger()
        {
            NegationExpression expr = new(new ConstantExpression(123));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            NegationExpression expr1 = new(new ConstantExpression(1));
            NegationExpression expr2 = new(new ConstantExpression(2));
            NegationExpression expr3 = new(new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
        }
    }
}
