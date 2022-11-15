using Microsoft.Azure.PowerShell.AssemblyLoading.Test.Mocks;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.Azure.PowerShell.AssemblyLoading.Test.UnitTests
{
    public class ConditionalAssemblyBuilderTests
    {
        [Fact]
        public void ShouldLoadAssemblyWithoutConstraint()
        {
            IConditionalAssemblyBuilder assemblyBuilder = new ConditionalAssemblyBuilder(null, "path", new Version(1, 0, 0));
            Assert.True(assemblyBuilder.ShouldLoad);
        }

        [Fact]
        public void ShouldLoadAssemblyAccordingToPSVersion()
        {
            // windows powershell
            var context = new MockConditionalAssemblyBuilderContext()
            { PSVersion = Version.Parse("5.1.22621.608") };
            var windowsPSAssembly = NewDummyAssembly(context).WithWindowsPowerShell();
            var psCoreAssembly = NewDummyAssembly(context).WithPowerShellCore();
            var neturalAssembly = NewDummyAssembly(context);
            Assert.True(windowsPSAssembly.ShouldLoad);
            Assert.False(psCoreAssembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);

            // powershell core and 7+
            context.PSVersion = Version.Parse("7.3.0");
            windowsPSAssembly = NewDummyAssembly(context).WithWindowsPowerShell();
            psCoreAssembly = NewDummyAssembly(context).WithPowerShellCore();
            neturalAssembly = NewDummyAssembly(context);
            Assert.False(windowsPSAssembly.ShouldLoad);
            Assert.True(psCoreAssembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);
        }

        [Fact]
        public void ShouldLoadAssemblyAccordingToOS()
        {
            // windows
            var context = new MockConditionalAssemblyBuilderContext()
            { OS = OSPlatform.Windows };
            var windowsAssembly = NewDummyAssembly(context).WithWindows();
            var linuxAssembly = NewDummyAssembly(context).WithLinux();
            var macOSAssembly = NewDummyAssembly(context).WithMacOS();
            var neturalAssembly = NewDummyAssembly(context);
            Assert.True(windowsAssembly.ShouldLoad);
            Assert.False(linuxAssembly.ShouldLoad);
            Assert.False(macOSAssembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);

            // linux
            context = new MockConditionalAssemblyBuilderContext()
            { OS = OSPlatform.Linux };
            windowsAssembly = NewDummyAssembly(context).WithWindows();
            linuxAssembly = NewDummyAssembly(context).WithLinux();
            macOSAssembly = NewDummyAssembly(context).WithMacOS();
            neturalAssembly = NewDummyAssembly(context);
            Assert.False(windowsAssembly.ShouldLoad);
            Assert.True(linuxAssembly.ShouldLoad);
            Assert.False(macOSAssembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);

            // macos
            context = new MockConditionalAssemblyBuilderContext()
            { OS = OSPlatform.OSX };
            windowsAssembly = NewDummyAssembly(context).WithWindows();
            linuxAssembly = NewDummyAssembly(context).WithLinux();
            macOSAssembly = NewDummyAssembly(context).WithMacOS();
            neturalAssembly = NewDummyAssembly(context);
            Assert.False(windowsAssembly.ShouldLoad);
            Assert.False(linuxAssembly.ShouldLoad);
            Assert.True(macOSAssembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);
        }

        [Fact]
        public void ShouldLoadAssemblyAccordingToCpuArch()
        {
            // x86
            var context = new MockConditionalAssemblyBuilderContext()
            { OSArchitecture = Architecture.X86 };
            var x86Assembly = NewDummyAssembly(context).WithX86();
            var x64Assembly = NewDummyAssembly(context).WithX64();
            var arm64Assembly = NewDummyAssembly(context).WithArm64();
            var neturalAssembly = NewDummyAssembly(context);
            Assert.True(x86Assembly.ShouldLoad);
            Assert.False(x64Assembly.ShouldLoad);
            Assert.False(arm64Assembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);

            // x64
            context = new MockConditionalAssemblyBuilderContext()
            { OSArchitecture = Architecture.X64 };
            x86Assembly = NewDummyAssembly(context).WithX86();
            x64Assembly = NewDummyAssembly(context).WithX64();
            arm64Assembly = NewDummyAssembly(context).WithArm64();
            neturalAssembly = NewDummyAssembly(context);
            Assert.False(x86Assembly.ShouldLoad);
            Assert.True(x64Assembly.ShouldLoad);
            Assert.False(arm64Assembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);

            // arm64
            context = new MockConditionalAssemblyBuilderContext()
            { OSArchitecture = Architecture.Arm64 };
            x86Assembly = NewDummyAssembly(context).WithX86();
            x64Assembly = NewDummyAssembly(context).WithX64();
            arm64Assembly = NewDummyAssembly(context).WithArm64();
            neturalAssembly = NewDummyAssembly(context);
            Assert.False(x86Assembly.ShouldLoad);
            Assert.False(x64Assembly.ShouldLoad);
            Assert.True(arm64Assembly.ShouldLoad);
            Assert.True(neturalAssembly.ShouldLoad);
        }

        [Fact]
        public void ShouldSupportMultipleConstraints()
        {
            // assembly requires powershell 7+ on linux
            // if both meet, it should be loaded
            var context = new MockConditionalAssemblyBuilderContext();
            context.OS = OSPlatform.Linux;
            context.PSVersion = Version.Parse("7.3.0");
            var assembly = NewDummyAssembly(context).WithLinux().WithPowerShellVersion(Version.Parse("7.0.0"));
            Assert.True(assembly.ShouldLoad);

            // otherwise it shouldn't
            context.OS = OSPlatform.Windows;
            context.PSVersion = Version.Parse("7.3.0");
            assembly = NewDummyAssembly(context).WithLinux().WithPowerShellVersion(Version.Parse("7.0.0"));
            Assert.False(assembly.ShouldLoad);

            context.OS = OSPlatform.Linux;
            context.PSVersion = Version.Parse("6.2.0");
            Assert.False(assembly.ShouldLoad);

            context.OS = OSPlatform.Windows;
            context.PSVersion = Version.Parse("5.1.0");
            Assert.False(assembly.ShouldLoad);
        }

        private static ConditionalAssemblyBuilder NewDummyAssembly(MockConditionalAssemblyBuilderContext context)
        {
            return new ConditionalAssemblyBuilder(context, "path", new Version(1, 0, 0));
        }
    }
}
