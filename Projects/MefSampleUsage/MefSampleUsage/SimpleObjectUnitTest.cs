using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MefSampleUsage
{
    [TestClass]
    public class SimpleObjectUnitTest : MefUnitTest
    {

        [TestMethod]
        public void Perfom_Simple_Object_Composition()
        {
            var toCompose = new ClassToCompose();
            Compose(toCompose);
            Assert.AreEqual(5, toCompose.PropertyToImport.IntegerProperty);
        }

        /// <summary>
        /// Test that will demonstrate instance behavior when composition happens more than once
        /// </summary>
        [TestMethod]
        public void Resolve_Multiple_Times()
        {
            
            var toCompose1 = new ClassToCompose();
            Compose(toCompose1);

            var toCompose2 = new ClassToCompose();
            Compose(toCompose2);

            // placeholders are different instances
            Assert.IsFalse(object.ReferenceEquals(toCompose1, toCompose2));
            // different placeholders have reference to same object
            Assert.IsTrue(object.ReferenceEquals(toCompose1.PropertyToImport, toCompose2.PropertyToImport));
        }

        [TestMethod]
        public void Get_Export_Directly_From_Container()
        {
            Lazy<ClassWithInteger> c = container.GetExport<ClassWithInteger>();
            var p = new ClassToCompose();
            Compose(p);

            Assert.IsTrue(Object.ReferenceEquals(c.Value, p.PropertyToImport));

        }



        /// <summary>
        /// class that will recieve the Mef Import
        /// </summary>
        public class ClassToCompose
        {

            // mef will set this property because of import attribute
            [Import]
            public ClassWithInteger PropertyToImport {get;set;}
        }

        /// <summary>
        /// Class with an integer value.  Class will be exported
        /// </summary>
        [Export]
        public class ClassWithInteger
        {

            public int IntegerProperty
            {
                get
                {
                    return 5;
                }
            }
        }

    }
}
