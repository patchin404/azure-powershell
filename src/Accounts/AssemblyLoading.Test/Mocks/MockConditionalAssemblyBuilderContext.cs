using System;
using System.Runtime.InteropServices;

namespace Microsoft.Azure.PowerShell.AssemblyLoading.Test.Mocks
{
    internal class MockConditionalAssemblyBuilderContext : IConditionalAssemblyBuilderContext
    {
        public Version PSVersion { get; set; }
        public Architecture OSArchitecture { get; set; }
        public OSPlatform OS { get; set; }

        public bool IsOSPlatform(OSPlatform os)
        {
            return OS.Equals(os);
        }
    }
}
