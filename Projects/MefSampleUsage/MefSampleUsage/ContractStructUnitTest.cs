using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace MefSampleUsage
{
    [TestClass]
    public class ContractStructUnitTest : MefUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ClassNeedingInteger c1 = new ClassNeedingInteger();
            Assert.AreEqual(0, c1.IntegerToImport);

            Compose(c1);
            Assert.AreEqual(5, c1.IntegerToImport);

        }

        public class ClassNeedingInteger
        {
            [Import("Medhat")]
            public int IntegerToImport { get; set; }
        }


        public class ClassWithInteger
        {
            [Export("Medhat")]
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
