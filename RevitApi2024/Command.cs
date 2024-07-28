using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Mechanical;
using RevitApi2024.DemoWpf;
using RevitApi2024.Shared;

namespace RevitApi2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();
            List<Line> lines = new List<Line>();
            foreach (ElementId id in selectedIds)
            {
                DetailCurve detailCurve = doc.GetElement(id) as DetailCurve;
                if (detailCurve != null)
                {
                    Curve curve = detailCurve.GeometryCurve as Curve;
                    Line lineNew = curve as Line;
                    if (lineNew != null)
                    {
                        lines.Add(lineNew);
                    }
                }
            }

            var allSystem = new FilteredElementCollector(doc).OfClass(typeof(MechanicalSystemType)).Cast<MechanicalSystemType>();

            ComboBoxWpf comboboxWpf = new ComboBoxWpf();
            comboboxWpf.comboboxSystemType.ItemsSource= allSystem;
            
            

            //var allSystem2 = new FilteredElementCollector(doc).OfClass(typeof(MEPSystemType)).Cast<MechanicalSystemType>();
            //ComboBoxWpf comboBoxWpf2 = new ComboBoxWpf();
            //comboBoxWpf2.comboboxDuctType.ItemsSource= allSystem;
            //comboBoxWpf2.ShowDialog();
            //MEPSystemType typeDuctChoose = comboBoxWpf2.comboboxSystemType.SelectedItem as MEPSystemType ;
            
            List<DuctType> ductTypes = new FilteredElementCollector(doc).OfClass(typeof(DuctType))
                .Cast<DuctType>().ToList();
            List<DuctTypeCustom> listDuctCustom = new List<DuctTypeCustom>();
            foreach (var ductType in ductTypes)
            {
                ElementId id = ductType.Id;
                string name = $"{ductType.FamilyName}: {ductType.Name} ";
                DuctTypeCustom ductTypeCustom = new DuctTypeCustom(id, name);
                listDuctCustom.Add(ductTypeCustom);
            }
            listDuctCustom = listDuctCustom.OrderBy(x=>x.Name).ToList();
            comboboxWpf.comboboxDuctType.ItemsSource= listDuctCustom;
            comboboxWpf.ShowDialog();
            MechanicalSystemType systemTypeChoose = comboboxWpf.comboboxSystemType.SelectedItem as MechanicalSystemType;
            DuctTypeCustom ductTypeCostumChoose = comboboxWpf.comboboxDuctType.SelectedItem as DuctTypeCustom;

            Level level = doc.ActiveView.GenLevel;

            foreach (Line line in lines)
            {
                using(Transaction t= new Transaction(doc, "CreateDuct"))
                {
                    t.Start();
                    XYZ start = line.GetEndPoint(0);
                    XYZ end = line.GetEndPoint(1);
                    Autodesk.Revit.DB.Mechanical.Duct.Create(doc, systemTypeChoose.Id, ductTypeCostumChoose.Id, level.Id, start,end);
                    t.Commit();
                }
                
            }

            return Result.Succeeded;
        }
    }
}
