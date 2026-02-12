# JakesCommands

A Revit add-in that rotates selected objects by **90°** or **180°**.

## Project layout

- `src/JakesCommands.RevitAddin` – Revit external application + command wiring.
- `src/JakesCommands.Core` – rotation angle/domain logic (pure .NET and unit testable).
- `tests/JakesCommands.Core.Tests` – xUnit tests for core logic.

## Build and test locally

```bash
dotnet build JakesCommands.sln
dotnet test JakesCommands.sln
```

> Note: this repository includes a lightweight mock of Revit API types so CI can compile without the proprietary Revit SDK. To run inside Revit, remove `MOCK_REVIT_API` from `JakesCommands.RevitAddin.csproj` and reference the real Revit API assemblies for your installed Revit version.

## Installing in Revit

1. Build `src/JakesCommands.RevitAddin` in Release mode.
2. Copy output DLLs to a local add-ins folder.
3. Create a `.addin` manifest (example below) and point to the compiled DLL/class names.

### Example `.addin` manifest

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Application">
    <Name>Jakes Commands</Name>
    <Assembly>C:\Path\To\JakesCommands.RevitAddin.dll</Assembly>
    <AddInId>3eacf836-3453-4c8c-a39f-3500d8f56f2a</AddInId>
    <FullClassName>JakesCommands.RevitAddin.JakesCommandsApp</FullClassName>
    <VendorId>JKCM</VendorId>
    <VendorDescription>Jake's custom Revit productivity commands</VendorDescription>
  </AddIn>
</RevitAddIns>
```

Once loaded, use the **Jakes Commands** ribbon tab and run **Rotate Selection**.
