# DynamicMSBuilder

The DynamicVars project contains a custom MSBuild task, UpdateAssemblyInfoTask, which updates the AssemblyInfo.cs file of a .NET project. The task generates new GUIDs and random strings for various assembly attributes, ensuring that each build of the project has unique metadata.

It is essentially a Dynamic MSBuild task to help with minor obfuscation of C# Binaries to evade static signatures on each compilation.

## Features
- Update GUID: Generates a new GUID for the assembly.
- Update Assembly Attributes: Randomize values for AssemblyTitle, AssemblyDescription, AssemblyCompany, and AssemblyProduct.
- Update Copyright Year: Randomize the year in the copyright statement.

## How To Use and Build
### Add the Task to Your Project
  - Ensure the DynmaicVars project is built and the DLL is accessible.
  - Reference this DLL in your project.

###  Modify Your Project File (.csproj)
  - Open your .csproj file in a text editor.
  - Compile DynmaicVars.cs to a Class Library, then include the DLL in the csproj to help with compilation, add it at the end just before the `</project>` tag:

```
<Project Sdk="Microsoft.NET.Sdk">

  <!-- rest of .csproj file -->

  <UsingTask TaskName="UpdateAssemblyInfoTask" AssemblyFile="$(OutputPath)DynmaicVars.dll"/>

  <Target Name="BeforeBuild">
    <UpdateAssemblyInfoTask FileToUpdate="Properties/AssemblyInfo.cs"/>
  </Target>

</Project>

```

### Build Your Project
  - When you build your project, the `BeforeBuild` target triggers the `UpdateAssemblyInfoTask`, updating the `AssemblyInfo.cs` file.
  - In Visual Studio locate the Solution Explorer, right click on "References" and select "Add Reference". You'll need the following:
    - Microsoft.Build.Framework
    - Microsoft.Build.Utilities
