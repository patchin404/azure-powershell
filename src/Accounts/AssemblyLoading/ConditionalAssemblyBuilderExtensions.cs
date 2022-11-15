using System;
using System.Runtime.InteropServices;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public static class ConditionalAssemblyBuilderExtensions
    {
        public static ConditionalAssemblyBuilder WithWindowsPowerShell(this ConditionalAssemblyBuilder builder)
        {
            return builder.WithPowerShellVersion(new Version("5.0.0"), new Version("6.0.0"));
        }
        public static ConditionalAssemblyBuilder WithPowerShellCore(this ConditionalAssemblyBuilder builder)
        {
            return builder.WithPowerShellVersion(new Version("6.0.0"));
        }
        public static ConditionalAssemblyBuilder WithPowerShellVersion(this ConditionalAssemblyBuilder builder, Version lower, Version upper = null)
        {
            builder.RefreshShouldLoad(lower <= builder.Context.PSVersion);
            if (upper != null)
            {
                builder.RefreshShouldLoad(builder.Context.PSVersion < upper);
            }
            return builder;
        }

        public static ConditionalAssemblyBuilder WithWindows(this ConditionalAssemblyBuilder builder)
            => builder.WithOS(OSPlatform.Windows);

        public static ConditionalAssemblyBuilder WithMacOS(this ConditionalAssemblyBuilder builder)
            => builder.WithOS(OSPlatform.OSX);

        public static ConditionalAssemblyBuilder WithLinux(this ConditionalAssemblyBuilder builder)
            => builder.WithOS(OSPlatform.Linux);

        private static ConditionalAssemblyBuilder WithOS(this ConditionalAssemblyBuilder builder, OSPlatform os)
        {
            builder.RefreshShouldLoad(builder.Context.IsOSPlatform(os));
            return builder;
        }

        public static ConditionalAssemblyBuilder WithX86(this ConditionalAssemblyBuilder builder)
            => builder.WithOSArchitecture(Architecture.X86);
        public static ConditionalAssemblyBuilder WithX64(this ConditionalAssemblyBuilder builder)
            => builder.WithOSArchitecture(Architecture.X64);
        public static ConditionalAssemblyBuilder WithArm64(this ConditionalAssemblyBuilder builder)
            => builder.WithOSArchitecture(Architecture.Arm64);

        private static ConditionalAssemblyBuilder WithOSArchitecture(this ConditionalAssemblyBuilder builder, Architecture arch)
        {
            builder.RefreshShouldLoad(builder.Context.OSArchitecture.Equals(arch));
            return builder;
        }
    }

}