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
            // crate class to compose
            var toCompose = new ToCompose();
            // perform composition
            Compose(toCompose);
            // integer value is set
            Assert.AreEqual(5, toCompose.PropertyToImport.IntegerValue);
        }

        #region Resolve Multiple Times Test


        /// <summary>
        /// Test that will demonstrate instance behavior when composition happens more than once
        /// </summary>
        [TestMethod]
        public void Resolve_Multiple_Times()
        {

            var toCompose1 = new ToCompose();
            Compose(toCompose1);

            var toCompose2 = new ToCompose();
            Compose(toCompose2);

            // placeholders are different instances
            Assert.IsFalse(object.ReferenceEquals(toCompose1, toCompose2));
            // different placeholders have reference to same object
            Assert.IsTrue(object.ReferenceEquals(toCompose1.PropertyToImport, toCompose2.PropertyToImport));
        }
        #endregion

        /// <summary>
        /// class that will recieve the Mef Import
        /// </summary>
        public class ToCompose
        {

            // mef will set this property because of import attribute
            [Import]
            public ClassWithInteger PropertyToImport { get; set; }
        }

        /// <summary>
        /// Class with an integer value.  Class will be exported
        /// </summary>
        [Export]
        public class ClassWithInteger
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
