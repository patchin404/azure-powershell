using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public class ConditionalAssemblyBuilderContext : IConditionalAssemblyBuilderContext
    {
        public ConditionalAssemblyBuilderContext(Version psVersion)
        {
            PSVersion = psVersion;
        }

        public static IConditionalAssemblyBuilderContext Instance { get; private set; }
        public static void Initialize(string psVersion)
        {
            Instance = new ConditionalAssemblyBuilderContext(new Version(psVersion));
        }

        public Version PSVersion { get; private set; }
        public bool IsOSPlatform(OSPlatform os) => RuntimeInformation.IsOSPlatform(os);
        public Architecture OSArchitecture => RuntimeInformation.OSArchitecture;
    }
}
