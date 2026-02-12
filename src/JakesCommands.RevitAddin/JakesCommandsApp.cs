using Autodesk.Revit.UI;

namespace JakesCommands.RevitAddin;

public class JakesCommandsApp : IExternalApplication
{
    public Result OnStartup(UIControlledApplication application)
    {
        const string tabName = "Jakes Commands";
        try
        {
            application.CreateRibbonTab(tabName);
        }
        catch
        {
            // Tab already exists.
        }

        RibbonPanel panel = application.CreateRibbonPanel(tabName, "Rotate");
        PushButtonData buttonData = new(
            "RotateSelection",
            "Rotate\nSelection",
            typeof(JakesCommandsApp).Assembly.Location,
            typeof(RotateSelectionCommand).FullName!);

        panel.AddItem(buttonData);
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
