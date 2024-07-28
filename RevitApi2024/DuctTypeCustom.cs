using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.Shared
{
    public class DuctTypeCustom
    {
        public DuctTypeCustom( ElementId id, string name )
        {
            Id = id; Name = name;
        }
        public ElementId Id {  get; set; }
        public string Name { get; set; }
    }
}
