using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using RevitApi2024.DemoWpf;
using System.Collections.Generic;
using System.Linq;

namespace RevitApi2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements, UIDocument uiDoc)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            var selectedIds = uiDoc.Selection.GetElementIds();

            List<MechanicalSystemType> ductSystem = new FilteredElementCollector(doc).OfClass(typeof(MechanicalSystemType)).
                Cast<MechanicalSystemType>().ToList();
            List<DuctType> ductTypes = new FilteredElementCollector(doc).OfClass(typeof(DuctType)).Cast<DuctType>().ToList();
            comboBox.comboDuctsys.ItemsSource = ductSystem;
            comboBox.comboxDuctType.ItemsSource = ductTypes;
            comboBox.ShowDialog();

            MechanicalSystemType selectedDuctsys = comboBox.comboDuctsys.SelectedItem as MechanicalSystemType;
            DuctType selectedDuctType = comboBox.comboxDuctType.SelectedItem as DuctType;

            List<Line> lines = new List<Line>();
            while (true)
            {
                try
                {
                    Reference r = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Pick Line");
                    DetailLine e = doc.GetElement(r) as DetailLine;
                    if (e != null) 
                    {
                        Curve curve = e.GeometryCurve as Curve;
                        lines.Add(curve as Line);
                    }
                }
                catch 
                {
                    break;
                };
            }
            
            List<Duct> ducts = new List<Duct>();

            foreach (Line line in lines)
            {
                using (Transaction t = new Transaction(doc, "Create Duct"))
                {

                }
            }


                //using (Transaction t = new Transaction(doc, "aaa"))
                //{
                //    t.Start();
                //    newType = typeSelect.Duplicate(nameType);
                //    t.Commit();
                //}
                //foreach (Line lineNew in lines)
                //{
                //    using (Transaction t = new Transaction(doc, "CreateDuct"))
                //    {
                //        t.Start();
                //        XYZ start = line.GetEndPoint(0);
                //        XYZ end = line.GetEndPoint(1);
                //        Autodesk.Revit.DB.Mechanical.Duct.Create(doc, systemTypeChoose.Id, ductCustomTypeChose.Id, level.Id, start, end);
                //        t.Commit();
                //    }
                //}

                //loc systemtype
                
            while (true)
            {
                try
                {
                    IList<Reference> r = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
                    Element e = doc.GetElement(r) as Element;
                    if (e is DetailLine)
                    {
                        DetailLine detailL = (DetailLine)e;
                        lines.Add(detailL.GeometryCurve as Line);
                    }

                }
                catch
                {
                    break;
                }

            }

            


            return Result.Succeeded;
        }
    }
}
