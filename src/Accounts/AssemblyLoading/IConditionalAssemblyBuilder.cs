using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public interface IConditionalAssemblyBuilder
    {
        bool ShouldLoad { get; }
        string Path { get; }
        Version Version { get; }
    }
}