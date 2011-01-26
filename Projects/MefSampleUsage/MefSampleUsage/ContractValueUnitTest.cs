using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace MefSampleUsage
{
    [TestClass]
    public class ContractValueUnitTest : MefUnitTest
    {
        [TestMethod]
        public void Compose_Contract_Value_Test()
        {
            // create instance to compose
            ToCompose c1 = new ToCompose();
            // Integer value is 0 (default)
            Assert.AreEqual(0, c1.IntegerValue);

            // perform composition
            Compose(c1);
            // Integer value is now 5
            Assert.AreEqual(8, c1.IntegerValue);

        }

        public class ToCompose
        {
            // Contract name is "Medhat"
            [Import("Medhat")]
            public int IntegerValue { get; set; }
        }


        public class ClassWithInteger
        {
            [Export("Medhat")]
            public int IntegerToExport
            {
                get
                {
                    return 8;
                }
            }

            [Export("Jeremiah")]
            public int IntegerToExport2
            {
                get
                {
                    return 100;
                }
            }
        }


    }
}
