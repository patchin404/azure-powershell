using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public static class ConditionalAssemblyCollection
    {
        private static IConditionalAssemblyBuilderContext _context;
        private static IEnumerable<IConditionalAssemblyBuilder> _assemblyBuilders;

        public static void Initialize(IConditionalAssemblyBuilderContext context)
        {
            _context = context;
            _assemblyBuilders = new List<IConditionalAssemblyBuilder>()
            {
                CreateAssembly("netcoreapp2.1/Azure.Core.dll", "1.25.0.0").WithPowerShellCore(),
                CreateAssembly("netstandard2.0/Azure.Identity.dll", "1.6.1.0"),
                CreateAssembly("netstandard2.0/Microsoft.Bcl.AsyncInterfaces.dll", "1.0.0.0"),
                CreateAssembly("netcoreapp2.1/Microsoft.Identity.Client.dll", "4.46.0.0").WithPowerShellCore(),
                CreateAssembly("netstandard2.0/Microsoft.Bcl.AsyncInterfaces.dll", "1.0.0.0"),
            };
        }

        private static ConditionalAssemblyBuilder CreateAssembly(string path, string version) => new ConditionalAssemblyBuilder(_context, path, version);

        public static IDictionary<string, Version> GetAssemblies()
        {
            return _assemblyBuilders.Where(x => x.ShouldLoad).ToDictionary(x => x.Path, x => x.Version);
        }
    }
}