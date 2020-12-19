using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF
{
    public static class MedFacConnect
    {
        public static MedFacSystemEntities context { get; set; }
        public static void Konst(string conn)
        {
            context = null;
            context = new MedFacSystemEntities(conn);
            
        }
    }
}
