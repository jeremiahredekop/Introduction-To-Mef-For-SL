using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace MefSampleUsage
{
    [TestClass]
    public class ImportingConstructorTest : MefUnitTest
    {
        [TestMethod]
        public void Compose_Using_Importing_Constructor()
        {
            
            // get exported Guid
            var exportedGuid = GetExportedGuid();

            // assert that the Guid has been exported
            Assert.AreEqual(exportedGuid, new Guid("6A8C8ADA-CF1C-406F-A966-6624D4F579BC"));

            // create instance to compose
            var toCompose = new ToCompose();
            // compose instance
            Compose(toCompose);

            // assert that the class we composed recieved the guid that was exported
            Assert.AreEqual(exportedGuid, toCompose.ClassDependingOnGuid.Value);
            Assert.AreEqual(exportedGuid, toCompose.ClassDependingOnGuid.AnotherGuid);

        }

        private Guid GetExportedGuid()
        {
            var exportedGuid = container.GetExport<Guid>().Value;
            return exportedGuid;
        }

        [TestMethod]
        public void Get_Export_Using_Importing_Constructor()
        {
            var export = container.GetExport<ClassDependingOnGuid>().Value;
            var exportedGuid = GetExportedGuid();
            Assert.AreEqual(exportedGuid,export.AnotherGuid);
            Assert.AreEqual(exportedGuid,export.Value);          
        }


        public class ToCompose
        {
            [Import]
            public ClassDependingOnGuid ClassDependingOnGuid { get; set; }
        }

        public class SimpleGuidContainer
        {
            [Export]
            public Guid SimpleGuid
            {
                get
                {
                    return new Guid("6A8C8ADA-CF1C-406F-A966-6624D4F579BC");
                }
            }

        }

        [Export]
        public class ClassDependingOnGuid
        {
            [ImportingConstructor]
            public ClassDependingOnGuid(Guid initialValue)
            {
                Value = initialValue;
            }

            public Guid Value
            {
                get;
                set;
            }

            [Import]
            public Guid AnotherGuid { get; set; }

        }
    }

        
}
