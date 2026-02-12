# JakesCommands

A Revit add-in that rotates selected objects by **90°** or **180°**.

## Jake install

Start by downloading the JakesCommands-Install.zip zip file from here: https://github.com/brandon-itpro/JakesCommands/releases/tag/v1.0.1
(Or individually download the JakesCommands.addin and JakesCommands.RevitAddin.dll files. 

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

## About MOCK_REVIT_API

`MOCK_REVIT_API` is CI test mode only.
Release packaging workflow builds **non-mock** so Jake gets the real add-in DLL.

## Repository layout

- `src/JakesCommands.RevitAddin` – Revit add-in command/app
- `src/JakesCommands.Core` – shared rotation logic
- `tests/JakesCommands.Core.Tests` – unit tests
- `deployment/JakesCommands.addin` – add-in manifest template
