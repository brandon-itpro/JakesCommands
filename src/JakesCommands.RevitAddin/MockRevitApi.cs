#if MOCK_REVIT_API
using System;
using System.Collections.Generic;

namespace Autodesk.Revit.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TransactionAttribute : Attribute
    {
        public TransactionAttribute(TransactionMode mode) { }
    }

    public enum TransactionMode
    {
        Manual
    }
}

namespace Autodesk.Revit.DB
{
    public class Document
    {
        public Element? GetElement(ElementId id) => new();
    }

    public class Element
    {
        public Location? Location { get; set; }
        public BoundingBoxXYZ? get_BoundingBox(object? view) => new() { Min = XYZ.Zero, Max = XYZ.Zero };
    }

    public class ElementId { }

    public class ElementSet : HashSet<Element> { }

    public abstract class Location { }

    public class LocationPoint : Location
    {
        public XYZ Point { get; set; } = XYZ.Zero;
    }

    public class BoundingBoxXYZ
    {
        public XYZ Min { get; set; } = XYZ.Zero;
        public XYZ Max { get; set; } = XYZ.Zero;
    }

    public class XYZ
    {
        public static XYZ Zero { get; } = new(0, 0, 0);
        public static XYZ BasisZ { get; } = new(0, 0, 1);

        public XYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public static XYZ operator +(XYZ a, XYZ b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static XYZ operator /(XYZ a, double scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);
    }

    public class Line
    {
        public static Line CreateBound(XYZ start, XYZ end) => new();
    }

    public static class ElementTransformUtils
    {
        public static void RotateElement(Document doc, ElementId id, Line axis, double radians) { }
    }

    public class Transaction : IDisposable
    {
        public Transaction(Document document, string name) { }
        public void Start() { }
        public void Commit() { }
        public void Dispose() { }
    }
}

namespace Autodesk.Revit.UI
{
    using Autodesk.Revit.DB;

    public enum Result
    {
        Succeeded,
        Cancelled,
        Failed
    }

    public interface IExternalCommand
    {
        Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements);
    }

    public interface IExternalApplication
    {
        Result OnStartup(UIControlledApplication application);
        Result OnShutdown(UIControlledApplication application);
    }

    public class ExternalCommandData
    {
        public UIApplication Application { get; } = new();
    }

    public class UIApplication
    {
        public UIDocument ActiveUIDocument { get; } = new();
    }

    public class UIDocument
    {
        public Document Document { get; } = new();
        public Selection Selection { get; } = new();
    }

    public class Selection
    {
        public ICollection<ElementId> GetElementIds() => new List<ElementId>();
    }

    public class TaskDialog
    {
        public static void Show(string title, string message) { }

        public TaskDialog(string title)
        {
        }

        public string MainInstruction { get; set; } = string.Empty;
        public string MainContent { get; set; } = string.Empty;

        public void AddCommandLink(TaskDialogCommandLinkId id, string text) { }

        public TaskDialogResult Show() => TaskDialogResult.CommandLink1;
    }

    public enum TaskDialogCommandLinkId
    {
        CommandLink1,
        CommandLink2
    }

    public enum TaskDialogResult
    {
        CommandLink1,
        CommandLink2
    }

    public class UIControlledApplication
    {
        public void CreateRibbonTab(string tabName) { }
        public RibbonPanel CreateRibbonPanel(string tabName, string panelName) => new();
    }

    public class RibbonPanel
    {
        public object AddItem(PushButtonData buttonData) => new();
    }

    public class PushButtonData
    {
        public PushButtonData(string name, string text, string assemblyName, string className)
        {
        }
    }
}
#endif
