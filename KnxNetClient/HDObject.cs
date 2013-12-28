using System;
using System.Collections.Generic;
using System.Text;

namespace Knx
{
    class HDObject
    {
        public String name { get; set; }
        public DateTime time { get; set; }
        public String unit { get; set; }

        public override String ToString()
        {
            String erg = time + ": " + name ;
            return erg;

        }

    }
}
