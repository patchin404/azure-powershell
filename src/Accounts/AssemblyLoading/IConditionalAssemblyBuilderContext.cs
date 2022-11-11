using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public interface IConditionalAssemblyBuilderContext
    {
        Version PSVersion { get; }
        bool IsOSPlatform(OSPlatform os);
        Architecture OSArchitecture { get; }
    }
}