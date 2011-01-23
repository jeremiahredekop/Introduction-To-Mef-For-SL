﻿using System;
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
            Assert.AreEqual(5, p.ClassWithInteger.IntegerToExport);
        }

        [TestMethod]
        public void TestResolve_Multiple_Times()
        {
            var p = new Placeholder();
            Compose(p);

            var p2 = new Placeholder();
            Compose(p2);

            // placeholders are different instances
            Assert.IsFalse(object.ReferenceEquals(p, p2));
            // different placeholders have reference to same object
            Assert.IsTrue(object.ReferenceEquals(p.ClassWithInteger, p2.ClassWithInteger));
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
            int IntegerToExport { get; }
        }

        public class Placeholder
        {

            [Import]
            public IClassWithInteger ClassWithInteger {get;set;}

        }

        [Export(typeof(IClassWithInteger))]
        public class ClassWithInteger : IClassWithInteger
        {

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
