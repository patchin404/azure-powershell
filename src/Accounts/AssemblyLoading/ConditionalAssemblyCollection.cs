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
                CreateAssembly("netcoreapp2.1/Microsoft.Identity.Client.dll", "4.46.0.0").WithPowerShellCore(),
                CreateAssembly("netcoreapp3.1/Microsoft.Identity.Client.Extensions.Msal.dll", "2.23.0.0").WithPowerShellCore(),

                CreateAssembly("netstandard2.0/Azure.Identity.dll", "1.6.1.0"),
                CreateAssembly("netstandard2.0/Microsoft.Bcl.AsyncInterfaces.dll", "1.0.0.0"),
                CreateAssembly("netstandard2.0/Microsoft.IdentityModel.Abstractions.dll", "6.22.1.0"),
                CreateAssembly("netstandard2.0/System.Memory.Data.dll", "1.0.2.0"),
                CreateAssembly("netstandard2.0/System.Text.Json.dll", "4.0.1.2"),
                CreateAssembly("netstandard2.0/System.Buffers.dll", "4.0.3.0").WithWindowsPowerShell(), // standard
                CreateAssembly("netstandard2.0/System.Memory.dll", "4.0.1.1").WithWindowsPowerShell(), // standard
                CreateAssembly("netstandard2.0/System.Net.Http.WinHttpHandler.dll", "4.0.2.0").WithWindowsPowerShell(), // standard
                CreateAssembly("netstandard2.0/System.Private.ServiceModel.dll", "4.7.0.0").WithWindowsPowerShell(), //standard
                CreateAssembly("netstandard2.0/System.Security.AccessControl.dll", "4.1.1.0").WithWindowsPowerShell(), //standard
                CreateAssembly("netstandard2.0/System.Security.Permissions.dll", "4.0.1.0").WithWindowsPowerShell(), //standard
                CreateAssembly("netstandard2.0/System.Security.Principal.Windows.dll", "4.1.1.0").WithWindowsPowerShell(),//standard
                CreateAssembly("netstandard2.0/System.ServiceModel.Primitives.dll", "4.7.0.0").WithWindowsPowerShell(), //standard
                CreateAssembly("netstandard2.0/System.Threading.Tasks.Extensions.dll", "4.2.0.1").WithWindowsPowerShell(), //standard

                CreateAssembly("netfx/Azure.Core.dll", "1.25.0.0").WithWindowsPowerShell(),
                //CreateAssembly("netfx/Azure.Identity.dll", "1.6.1.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/Microsoft.Bcl.AsyncInterfaces.dll", "1.1.1.0").WithWindowsPowerShell(), //need?
                CreateAssembly("netfx/Microsoft.Identity.Client.dll", "4.46.2.0").WithWindowsPowerShell(), //4.46.0.0?
                CreateAssembly("netfx/Microsoft.Identity.Client.Extensions.Msal.dll", "2.23.0.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/Microsoft.IdentityModel.Abstractions.dll", "6.22.1.0").WithWindowsPowerShell(), //need?
                CreateAssembly("netfx/Newtonsoft.Json.dll", "10.0.0.0").WithWindowsPowerShell(), //double check; 12.0.0
                CreateAssembly("netfx/System.Diagnostics.DiagnosticSource.dll", "4.0.4.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Memory.Data.dll", "1.0.2.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Numerics.Vectors.dll", "4.1.4.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Reflection.DispatchProxy.dll", "4.0.4.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Runtime.CompilerServices.Unsafe.dll", "4.0.6.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Security.Cryptography.Cng.dll", "4.3.0.0").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Text.Encodings.Web.dll", "4.0.5.1").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Text.Json.dll", "4.0.1.2").WithWindowsPowerShell(),
                CreateAssembly("netfx/System.Xml.ReaderWriter.dll", "4.1.0.0").WithWindowsPowerShell(),
            };
        }

        private static ConditionalAssemblyBuilder CreateAssembly(string path, string version) => new ConditionalAssemblyBuilder(_context, path, version);

        public static IDictionary<string, Version> GetAssemblies()
        {
            return _assemblyBuilders.Where(x => x.ShouldLoad).ToDictionary(x => x.Path, x => x.Version);
        }
    }
}