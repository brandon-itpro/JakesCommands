# JakesCommands

A Revit add-in that rotates selected objects by **90°** or **180°**.

---

## For Jake (non-programmer): easiest way to install

Yes — if you want this to be easy for Jake, you should publish a **GitHub Release** each time you update the add-in.

### Why a Release helps
A Release gives Jake a single ZIP download. He does **not** need Visual Studio, Git, or command-line tools.

### What you should do on GitHub now
1. Go to your repo on GitHub.
2. Click **Releases** → **Draft a new release**.
3. Create a tag like `v1.0.0`.
4. In this repo, upload these files as release assets:
   - `JakesCommands.RevitAddin.dll`
   - `JakesCommands.addin`
   - (Optional but recommended) `JakesCommands-Install.zip` that includes both files and a 1-page install note.
5. Publish release.

> See **How to create the two release files** below for exact build steps.

After this, Jake only downloads and copies files to one folder.

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


## How to create the two release files

This section is for the person packaging the add-in (you/IT/developer).

### File 1: `JakesCommands.RevitAddin.dll`

Build the Revit add-in in **Release** with real Revit API DLL references (not mock mode):

```powershell
$env:REVIT_API_DIR = "C:\Program Files\Autodesk\Revit 2024"
dotnet build .\src\JakesCommands.RevitAddin\JakesCommands.RevitAddin.csproj -c Release
```

The output DLL will be here:

```text
src\JakesCommands.RevitAddin\bin\Release\net8.0\JakesCommands.RevitAddin.dll
```

### File 2: `JakesCommands.addin`

Use the ready template in this repo:

```text
deployment\JakesCommands.addin
```

Open it and set `<Assembly>` to the final path where `JakesCommands.RevitAddin.dll` will live on Jake's machine.

### Important: MOCK mode and real Revit

`MOCK_REVIT_API` is only for CI builds on GitHub.
Do **not** use mock mode for Jake's production DLL.

If you build without `-p:MOCK_REVIT_API=true`, the project uses real `RevitAPI.dll` / `RevitAPIUI.dll` from `REVIT_API_DIR`.

## Quick troubleshooting (for non-programmers)

### I don’t see “Jakes Commands” tab
- Make sure `.dll` and `.addin` are in the same Addins year folder.
- Make sure the year folder matches your Revit version.
- Close Revit fully and reopen.

### Revit says it can’t load the add-in
- Right-click downloaded `.dll` → **Properties** → if there is an **Unblock** checkbox, check it.
- Confirm `.addin` file points to the exact DLL path.

### Still not working
Send IT or the developer:
- Revit version (year)
- screenshot of `%AppData%\Autodesk\Revit\Addins\<year>` contents
- exact error message popup

---

## For developer/IT: build and package

## Project layout

- `src/JakesCommands.RevitAddin` – Revit external application + command wiring.
- `src/JakesCommands.Core` – rotation angle/domain logic (pure .NET and unit testable).
- `tests/JakesCommands.Core.Tests` – xUnit tests for core logic.

## Build and test locally

```bash
dotnet build JakesCommands.sln
dotnet test JakesCommands.sln
```

> Note: CI uses `MOCK_REVIT_API=true` so GitHub can compile without proprietary Revit SDK binaries. Real Revit deployment should be built without mock mode and with `REVIT_API_DIR` pointing to your Revit install folder.

## Example `.addin` manifest

A ready file is included at `deployment/JakesCommands.addin`. If needed, use this template and adjust `<Assembly>`:

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
