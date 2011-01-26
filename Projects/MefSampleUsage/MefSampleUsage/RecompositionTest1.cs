using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MefSampleUsage
{
    [TestClass]
    public class RecompositionTest1
    {
        [TestMethod]
        public void Test_Recomposition_With_ComposeParts()
        {
            // create aggregate catalog
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            // create container
            CompositionContainer container = new CompositionContainer(aggregateCatalog);

            // there are no parts in the container yet..
            Assert.AreEqual(0, aggregateCatalog.Parts.Count());

            // make placeholder instance
            var placeholder = new ToCompose();
            // GuidClassCollection is null
            Assert.IsNull(placeholder.GuidClassCollection);

            // compose placeholder using container
            // (allowRecomposition=true)
            container.ComposeParts(placeholder);


            // GuidClassCollection is null
            Assert.IsNotNull(placeholder.GuidClassCollection);
            // GuidClassCollection has been set by mef (not null)
            Assert.AreEqual(1, placeholder.GuidClassCollection_SetCount);
            // since there are no parts, colleciton should be empty
            Assert.AreEqual(0, placeholder.GuidClassCollection.Count());


            // now put 1 part into aggregate catalog
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(ClassWithGuid1)));
            // there are no parts, empty list
            Assert.AreEqual(1, placeholder.GuidClassCollection.Count());
            // GuidClassCollection array has been set again by MEF
            Assert.AreEqual(2, placeholder.GuidClassCollection_SetCount);

            // store GuidID for the instance of the Exported Part
            var class1Guid = placeholder.GuidClassCollection.Single().GuidValue;

            // now put a second part into aggregate catalog
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(ClassWithGuid2)));
            // there are no parts, empty list
            Assert.AreEqual(2, placeholder.GuidClassCollection.Count());
            // GuidClassCollection array has been set again by MEF
            Assert.AreEqual(3, placeholder.GuidClassCollection_SetCount);

            // after recomposition, check that the original stored Guid value matches exported part
            Assert.AreEqual(class1Guid, placeholder.GuidClassCollection.Single(o => o is ClassWithGuid1).GuidValue);



        }


        [TestMethod]
        public void Test_Recomposition2_With_GetExport()
        {
            // create aggregate catalog
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            // create container
            CompositionContainer container = new CompositionContainer(aggregateCatalog);

            // there are no parts in the container yet..
            Assert.AreEqual(0, aggregateCatalog.Parts.Count());

            // make placeholder instance
            var placeholder = new ToCompose();
            // GuidClassCollection is null
            Assert.IsNull(placeholder.GuidClassCollection);

            // now put 1 part into aggregate catalog
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(ClassWithGuid1), typeof(ToCompose)));

            placeholder = container.GetExportedValue<ToCompose>();

            // now put a second part into aggregate catalog
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(ClassWithGuid2)));
            // there are no parts, empty list
            Assert.AreEqual(2, placeholder.GuidClassCollection.Count());


        }




        [Export]
        public class ToCompose
        {
            /// <summary>
            /// Tracks number of times GuidClassCollection is set
            /// </summary>
            public int GuidClassCollection_SetCount { get; set; }


            private IEnumerable<IClassWithGuid> _guidClassCollection;


            [ImportMany(AllowRecomposition = true)]
            public IEnumerable<IClassWithGuid> GuidClassCollection
            {
                get
                {
                    return _guidClassCollection;
                }
                set
                {
                    _guidClassCollection = value;
                    GuidClassCollection_SetCount++;
                }

            }


        }

        [Export(typeof(IClassWithGuid))]
        public class ClassWithGuid1 : IClassWithGuid
        {

            public ClassWithGuid1()
            {
                GuidValue = Guid.NewGuid();
            }

            public Guid GuidValue { get; private set; }
        }


        [Export(typeof(IClassWithGuid))]
        public class ClassWithGuid2 : IClassWithGuid
        {

            public ClassWithGuid2()
            {
                GuidValue = Guid.NewGuid();
            }

            public Guid GuidValue { get; private set; }
        }


        public interface IClassWithGuid
        {
            Guid GuidValue { get; }
        }


    }
}
