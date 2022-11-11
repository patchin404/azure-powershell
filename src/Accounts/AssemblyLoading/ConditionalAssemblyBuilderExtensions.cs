using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public static class ConditionalAssemblyBuilderExtensions
    {
        public static ConditionalAssemblyBuilder WithWindowsPowerShell(this ConditionalAssemblyBuilder builder)
        {
            return builder.WithPowerShellVersion("5.0.0", "6.0.0");
        }
        public static ConditionalAssemblyBuilder WithPowerShellCore(this ConditionalAssemblyBuilder builder)
        {
            return builder.WithPowerShellVersion("6.0.0");
        }
        internal static ConditionalAssemblyBuilder WithPowerShellVersion(this ConditionalAssemblyBuilder builder, string lower, string upper = null)
        {
            builder.RefreshShouldLoad(new Version(lower) <= builder.Context.PSVersion && builder.Context.PSVersion < new Version(upper));
            return builder;
        }

        public static ConditionalAssemblyBuilder WithOS(this ConditionalAssemblyBuilder builder, OSPlatform os)
        {
            builder.RefreshShouldLoad(builder.Context.IsOSPlatform(os));
            return builder;
        }
        public static ConditionalAssemblyBuilder WithOSArchitecture(this ConditionalAssemblyBuilder builder, Architecture arch)
        {
            builder.RefreshShouldLoad(builder.Context.OSArchitecture.Equals(arch));
            return builder;
        }
    }

}