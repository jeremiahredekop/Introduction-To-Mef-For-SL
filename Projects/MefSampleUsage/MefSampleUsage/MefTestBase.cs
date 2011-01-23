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
    public class MefUnitTest
    {

        public MefUnitTest()
        {
            var cat = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            container = new CompositionContainer(cat);
        }

        protected CompositionContainer container;

        protected void Compose(object toCompose)
        {
            container.ComposeParts(toCompose);
        }

    }
}
