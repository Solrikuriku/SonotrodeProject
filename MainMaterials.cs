using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonotrodeProject
{
    public class MainMaterials
    { 
        public string Name { get; set; }
        public int AmplitudeStart { get; set; }
        public int AmplitudeEnd { get; set; }
        public MainMaterials() { }
        public MainMaterials(string name, int aStart, int aEnd)
        {
            Name = name;
            AmplitudeStart = aStart;
            AmplitudeEnd = aEnd;
        }

        /*
         * public override string ToString()
        {
            return "\tMainMaterials@" + GetHashCode() + "{\r\n" +
                "\t\tName: " + Name + "\r\n" +
                "\t\tAmplitudeStart: " + AmplitudeStart + "\r\n" +
                "\t\tAmplitudeEnd: " + AmplitudeEnd + "\r\n" +
                "\t}";

        }
        */
    }
}
