using System;
using System.Collections.Generic;
using System.Text;

namespace Knx
{
    class EIS11 : HDKnx
    {

        public EIS11()
        {
        }

        public EIS11(cEMI emi) : base(emi)
        {
            value = emi.Eis11;
        }

        public uint value { get; set; }

        public override void SetValue(cEMI emi)
        {
            base.SetValue(emi);
            value = emi.Eis11;
        }


        public  void SetValue(uint val)
        {
            value = val;
            time = DateTime.Now;

            rawValue = new byte[5];
            rawValue[0] = 0;
            rawValue[1] = (byte)(value >> 24);
            rawValue[2] = (byte)((value >> 16) & 0xFF);
            rawValue[3] = (byte)((value >> 8) & 0xFF);
            rawValue[4] = (byte)((value) & 0xFF);
        }

        public override String ToString()
        {
            String erg = base.ToString() + "  EIS11 = " + value.ToString();
            return erg;
        }

    }
}
