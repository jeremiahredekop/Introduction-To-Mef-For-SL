using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace MefSampleUsage
{
    /// <summary>
    /// Base class for unit tests that encapsulate MEF Assembly catalog & container
    /// </summary>
    public class MefUnitTest
    {

        public MefUnitTest()
        {
            // create catalog to use current assembly
            var cat = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            // create container instance
            container = new CompositionContainer(cat);
        }

        // container instance
        protected CompositionContainer container;

        /// <summary>
        /// Compose object
        /// </summary>
        /// <param name="toCompose">object that composition should be performed on</param>
        protected void Compose(object toCompose)
        {
            container.ComposeParts(toCompose);
        }

    }
}
