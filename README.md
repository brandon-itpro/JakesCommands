# JakesCommands

A Revit add-in that rotates selected objects by **90°** or **180°**.

---

## Jake’s install steps (Windows + Revit)

### 1) Download
- Open the repo’s **Latest Release** page.
- Download `JakesCommands-Install.zip` (or download `JakesCommands.RevitAddin.dll` + `JakesCommands.addin`).

### 2) Copy files
Copy the two files into this folder (create it if needed):

```text
%AppData%\Autodesk\Revit\Addins\2024\
```

> If you use a different Revit year, replace `2024` with your year (for example `2023` or `2025`).

You should end up with:

```text
%AppData%\Autodesk\Revit\Addins\2024\JakesCommands.RevitAddin.dll
%AppData%\Autodesk\Revit\Addins\2024\JakesCommands.addin
```

### 3) Start Revit
- Open Revit.
- Look for a ribbon tab named **Jakes Commands**.
- Click **Rotate Selection**.

### 4) Use it
- Select object(s).
- Run **Rotate Selection**.
- Pick **90°** or **180°** in the dialog.

---

## Quick troubleshooting (for non-programmers)

### I don’t see “Jakes Commands” tab
- Make sure `.dll` and `.addin` are in the same Addins year folder.
- Make sure the year folder matches your Revit version.
- Close Revit fully and reopen.

### Revit says it can’t load the add-in
- Right-click downloaded `.dll` → **Properties** → if there is an **Unblock** checkbox, check it.
- Confirm `.addin` file points to the exact DLL path.

### Still not working
Send Brandon:
- Revit version (year)
- screenshot of `%AppData%\Autodesk\Revit\Addins\<year>` contents
- exact error message popup

---

## For Brandon: build and package

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

## Example `.addin` manifest

Create `JakesCommands.addin` and adjust the `<Assembly>` path:

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
