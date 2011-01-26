using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MefSampleUsage
{
    [TestClass]
    public class SimpleValueUnitTest : MefUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // create object to compose
            ToCompose c1 = new ToCompose();
            // Integer value is 0 (default)
            Assert.AreEqual(0, c1.IntegerValue);

            // perform composition
            Compose(c1);
            // Integer value is now 5
            Assert.AreEqual(5, c1.IntegerValue);
        }

        public class ToCompose
        {
            [Import]
            public int IntegerValue { get; set; }
        }


        public class ClassWithInteger
        {
            [Export]
            public int IntegerToExport
            {
                get
                {
                    return 5;
                }
            }
        }


    }
}
