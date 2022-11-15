using System;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public interface IConditionalAssemblyBuilder
    {
        bool ShouldLoad { get; }
        string Name { get; }
        string Framework { get; }
        Version Version { get; }
    }
}