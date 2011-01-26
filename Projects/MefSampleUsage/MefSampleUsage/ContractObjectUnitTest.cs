using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MefSampleUsage
{
    [TestClass]
    public class ContractObjectUnitTest : MefUnitTest
    {

        [TestMethod]
        public void TestCompose()
        {
            var p = new Placeholder();
            Compose(p);
            Assert.AreEqual(5, p.ClassWithInteger.IntegerValue);
        }

        [TestMethod]
        public void Test_Get_Export()
        {
            Lazy<IClassWithInteger> c = container.GetExport<IClassWithInteger>();
            var p = new Placeholder();
            Compose(p);

            Assert.IsTrue(Object.ReferenceEquals(c.Value, p.ClassWithInteger));

        }

        public interface IClassWithInteger
        {
            int IntegerValue { get; }
        }

        public class Placeholder
        {

            [Import]
            public IClassWithInteger ClassWithInteger {get;set;}

        }

        [Export(typeof(IClassWithInteger))]
        public class ClassWithInteger : IClassWithInteger
        {

            public int IntegerValue
            {
                get
                {
                    return 5;
                }
            }
        }

    }
}
