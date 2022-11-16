// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Azure.PowerShell.AssemblyLoading
{
    public static class ConditionalAssemblyExtensions
    {
        public static ConditionalAssembly WithWindowsPowerShell(this ConditionalAssembly assembly)
        {
            return assembly.WithPowerShellVersion(new Version("5.0.0"), new Version("6.0.0"));
        }
        public static ConditionalAssembly WithPowerShellCore(this ConditionalAssembly assembly)
        {
            return assembly.WithPowerShellVersion(new Version("6.0.0"));
        }
        public static ConditionalAssembly WithPowerShellVersion(this ConditionalAssembly assembly, Version lower, Version upper = null)
        {
            assembly.RefreshShouldLoad(lower <= assembly.Context.PSVersion);
            if (upper != null)
            {
                assembly.RefreshShouldLoad(assembly.Context.PSVersion < upper);
            }
            return assembly;
        }

        public static ConditionalAssembly WithWindows(this ConditionalAssembly assembly)
            => assembly.WithOS(OSPlatform.Windows);

        public static ConditionalAssembly WithMacOS(this ConditionalAssembly assembly)
            => assembly.WithOS(OSPlatform.OSX);

        public static ConditionalAssembly WithLinux(this ConditionalAssembly assembly)
            => assembly.WithOS(OSPlatform.Linux);

        private static ConditionalAssembly WithOS(this ConditionalAssembly assembly, OSPlatform os)
        {
            assembly.RefreshShouldLoad(assembly.Context.IsOSPlatform(os));
            return assembly;
        }

        public static ConditionalAssembly WithX86(this ConditionalAssembly assembly)
            => assembly.WithOSArchitecture(Architecture.X86);
        public static ConditionalAssembly WithX64(this ConditionalAssembly assembly)
            => assembly.WithOSArchitecture(Architecture.X64);
        public static ConditionalAssembly WithArm64(this ConditionalAssembly assembly)
            => assembly.WithOSArchitecture(Architecture.Arm64);

        private static ConditionalAssembly WithOSArchitecture(this ConditionalAssembly assembly, Architecture arch)
        {
            assembly.RefreshShouldLoad(assembly.Context.OSArchitecture.Equals(arch));
            return assembly;
        }
    }

}