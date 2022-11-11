using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public interface IConditionalAssemblyCollection
    {
        void Add(params IConditionalAssemblyBuilder[] conditionalAssemblyBuilder);
        IDictionary<string, Version> GetAssemblies();
    }
}