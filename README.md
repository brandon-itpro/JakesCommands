# JakesCommands

A Revit add-in that rotates selected objects by **90°** or **180°**.

## What you want (no local build)

You do **not** need a dev machine.

Use GitHub Actions to generate these files for Jake:
- `JakesCommands.RevitAddin.dll`
- `JakesCommands.addin`
- `JakesCommands-Install.zip`

## One-click way to get the files

1. Open GitHub repo → **Actions**.
2. Run workflow: **release-artifacts** (button: **Run workflow**).
3. Open that run when it finishes.
4. Download artifact: **JakesCommands-Install**.
5. Give Jake `JakesCommands-Install.zip` (or the two files directly).

## Jake install

Jake copies these two files into:

```text
%AppData%\Autodesk\Revit\Addins\2024\
```

Files:

```text
JakesCommands.RevitAddin.dll
JakesCommands.addin
```

Then restart Revit.

If you previously copied extra files, remove any old `JakesCommands.RevitAddin.dll.config` file from that folder before restarting Revit.

## Important fix for the “Assembly Not Found” popup

If Revit shows an error like:

- `assembly "%AppData%\Autodesk\...\JakesCommands.RevitAddin.dll" does not exist`

that means Revit is reading `%AppData%` as literal text from the `.addin` file (not expanding it on that machine).

This repo now uses a safer manifest value:

```xml
<Assembly>JakesCommands.RevitAddin.dll</Assembly>
```

That works when both files are in the same folder:

```text
%AppData%\Autodesk\Revit\Addins\2024\
```

So Jake should place exactly these two files together in that folder:

- `JakesCommands.RevitAddin.dll`
- `JakesCommands.addin`

## About MOCK_REVIT_API

`MOCK_REVIT_API` is CI test mode only.
Release packaging workflow builds **non-mock** so Jake gets the real add-in DLL.

## Repository layout

- `src/JakesCommands.RevitAddin` – Revit add-in command/app
- `src/JakesCommands.Core` – shared rotation logic
- `tests/JakesCommands.Core.Tests` – unit tests
- `deployment/JakesCommands.addin` – add-in manifest template
