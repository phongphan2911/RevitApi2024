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
            var allSystem = new FilteredElementCollector(doc).OfClass(typeof(MEPSystemType)).Cast<MechanicalSystemType>();

            ComboBoxWpf comboboxWpf = new ComboBoxWpf();
            comboboxWpf.comboboxSystemType.ItemsSource= allSystem;
            comboboxWpf.ShowDialog();
            MEPSystemType systemTypeChoose = comboboxWpf.comboboxSystemType.SelectedItem as MEPSystemType;

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

            return Result.Succeeded;
        }
    }
}
