using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public class ConditionalAssemblyBuilder : IConditionalAssemblyBuilder
    {
        public ConditionalAssemblyBuilder(IConditionalAssemblyBuilderContext context, string path, string version)
        {
            Context = context;
            _path = path;
            _version = new Version(version);
        }

        public bool ShouldLoad { get; private set; }
        internal void RefreshShouldLoad(bool shouldLoad)
        {
            if (ShouldLoad)
                ShouldLoad = shouldLoad;
        }

        internal IConditionalAssemblyBuilderContext Context;

        public string Path => _path;
        private string _path;

        public Version Version => _version;
        private Version _version;
    }
}