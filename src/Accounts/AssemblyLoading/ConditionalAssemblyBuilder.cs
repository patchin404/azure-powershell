using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public class ConditionalAssemblyBuilder : IConditionalAssemblyBuilder
    {
        public ConditionalAssemblyBuilder(IConditionalAssemblyBuilderContext context, string name, string framework, Version version)
        {
            Context = context;
            Name = name;
            Framework = framework;
            Version = version;
            ShouldLoad = true;
        }

        public bool ShouldLoad { get; private set; }
        internal void RefreshShouldLoad(bool shouldLoad)
        {
            if (ShouldLoad)
                ShouldLoad = shouldLoad;
        }

        internal IConditionalAssemblyBuilderContext Context;

        public Version Version { get; }

        public string Name { get; }

        public string Framework { get; }

    }
}