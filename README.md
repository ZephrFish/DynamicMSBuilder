# DynamicMSBuilder
Compile DynmaicVars.cs to a Class Library then include the DLL in the csproj to help at compilation:

```
<Project Sdk="Microsoft.NET.Sdk">

  <!-- Other configurations -->

  <UsingTask TaskName="UpdateAssemblyInfoTask" AssemblyFile="$(OutputPath)DynmaicVars.dll"/>

  <Target Name="BeforeBuild">
    <UpdateAssemblyInfoTask FileToUpdate="Properties/AssemblyInfo.cs"/>
  </Target>

</Project>

```