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
        public void Contract_Object_Composition_Test()
        {
            // create instance to compose
            var p = new ToCompose();
            // perform composition
            Compose(p);
            // value is set
            Assert.AreEqual(5, p.ClassWithInteger.IntegerValue);
        }

        [TestMethod]
        public void Test_Get_Export()
        {
            Lazy<IClassWithInteger> c = container.GetExport<IClassWithInteger>();
            var p = new ToCompose();
            Compose(p);

            Assert.IsTrue(Object.ReferenceEquals(c.Value, p.ClassWithInteger));

        }

        public interface IClassWithInteger
        {
            int IntegerValue { get; }
        }

        public class ToCompose
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
