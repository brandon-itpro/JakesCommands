using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JakesCommands.Core;

namespace JakesCommands.RevitAddin;

[Transaction(TransactionMode.Manual)]
public class RotateSelectionCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        return ExecuteCore(commandData);
    }

    private static Result ExecuteCore(ExternalCommandData commandData)
    {
        UIDocument uiDocument = commandData.Application.ActiveUIDocument;
        Document document = uiDocument.Document;

        ICollection<ElementId> selectedIds = uiDocument.Selection.GetElementIds();
        if (selectedIds.Count == 0)
        {
            TaskDialog.Show("Jakes Commands", "Select at least one element before running this command.");
            return Result.Cancelled;
        }

        RotationOption rotationOption = AskRotationOption();
        double radians = RotationHelper.ToRadians(rotationOption);

        using Transaction tx = new(document, $"Rotate {selectedIds.Count} element(s) by {(int)rotationOption}°");
        tx.Start();

        foreach (ElementId id in selectedIds)
        {
            Element? element = document.GetElement(id);
            if (element is null)
            {
                continue;
            }

            XYZ pivot = ResolvePivotPoint(element);
            Line axis = Line.CreateBound(pivot, pivot.Add(XYZ.BasisZ));
            ElementTransformUtils.RotateElement(document, id, axis, radians);
        }

        tx.Commit();
        return Result.Succeeded;
    }

    private static RotationOption AskRotationOption()
    {
        TaskDialog dialog = new("Jakes Commands")
        {
            MainInstruction = "Choose a rotation angle",
            MainContent = "Rotate selected elements by 90° or 180° around each element's center point."
        };

        dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Rotate 90°");
        dialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Rotate 180°");

        TaskDialogResult result = dialog.Show();
        return result == TaskDialogResult.CommandLink2 ? RotationOption.Rotate180 : RotationOption.Rotate90;
    }

    private static XYZ ResolvePivotPoint(Element element)
    {
        if (element.Location is LocationPoint locationPoint)
        {
            return locationPoint.Point;
        }

        BoundingBoxXYZ? bbox = element.get_BoundingBox(null);
        if (bbox is null)
        {
            return XYZ.Zero;
        }

        return new XYZ(
            (bbox.Min.X + bbox.Max.X) / 2.0,
            (bbox.Min.Y + bbox.Max.Y) / 2.0,
            (bbox.Min.Z + bbox.Max.Z) / 2.0);
    }
}
