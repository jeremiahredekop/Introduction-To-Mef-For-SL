using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MefSampleUsage
{
    [TestClass]
    public class ContractObjectUnitTestWithMetadata : MefUnitTest
    {

        [TestMethod]
        public void TestCompose()
        {
            var p = new Placeholder();
            Compose(p);
            Assert.AreEqual(7, p.ClassWithInteger.IntegerToExport);
        }


        [TestMethod]
        public void Test_Get_Exports()
        {
            IEnumerable<Lazy<IClassWithInteger,ISampleMetadata>> c = container.GetExports<IClassWithInteger,ISampleMetadata>();

            var results = from t in c
                          select new { t.Metadata.Name, t.Value };

            Assert.AreEqual(1,results.Count(a => a.Name == "Jeremiah" && a.Value.IntegerToExport == 7));
            Assert.AreEqual(1, results.Count(a => a.Name == "Medhat" && a.Value.IntegerToExport == 10));
        }

        public interface IClassWithInteger
        {
            int IntegerToExport { get; }
        }

        public class Placeholder
        {

            [ImportMany]
            public List<Lazy<IClassWithInteger,ISampleMetadata>> ClassWithIntegerImport {get;set;}

            public IClassWithInteger ClassWithInteger
            {
                get
                {
                    return ClassWithIntegerImport.OrderBy(o => o.Metadata.Name).First().Value;
                }
            }

        }

        public interface ISampleMetadata
        {
            string Name { get; }
        }

        [ExportMetadata("Name","Medhat")]
        [Export(typeof(IClassWithInteger))]
        public class ClassWithIntegerA : IClassWithInteger
        {

            public int IntegerToExport
            {
                get
                {
                    return 10;
                }
            }
        }

        [ExportMetadata("Name", "Jeremiah")]
        [Export(typeof(IClassWithInteger))]
        public class ClassWithIntegerB : IClassWithInteger
        {

            public int IntegerToExport
            {
                get
                {
                    return 7;
                }
            }
        }

    }
}
