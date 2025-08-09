using Embers.Expressions;

namespace Embers.Tests.Expressions
{
    [TestClass]
    public class CompareExpressionTests
    {
        [TestMethod]
        public void CompareEqualIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNotEqualIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.NotEqual);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareLessIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Less);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareGreaterIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Greater);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareGreaterOrEqualIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.GreaterOrEqual);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareLessOrEqualIntegers()
        {
            CompareExpression expr = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.LessOrEqual);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            CompareExpression expr1 = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);
            CompareExpression expr2 = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.NotEqual);
            CompareExpression expr3 = new(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
        }
    }
}
