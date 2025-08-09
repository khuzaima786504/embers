using Embers.StdLib;

namespace Embers.Tests.StdLib
{
    [TestClass]
    public class PutsFunctionTests
    {
        [TestMethod]
        public void PutsInteger()
        {
            StringWriter writer = new();
            PutsFunction function = new(writer);

            Assert.IsNull(function.Apply(null, null, [123]));

            Assert.AreEqual("123\r\n", writer.ToString());
        }

        [TestMethod]
        public void PutsTwoIntegers()
        {
            StringWriter writer = new();
            PutsFunction function = new(writer);

            Assert.IsNull(function.Apply(null, null, [123, 456]));

            Assert.AreEqual("123\r\n456\r\n", writer.ToString());
        }
    }
}
