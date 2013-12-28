using System;
using System.Collections.Generic;
using System.Text;

namespace Knx
{
    class EIS1 : HDKnx
    {

        public EIS1()
        {
        }

        public EIS1(cEMI emi) : base(emi)
        {
            value = emi.Eis1;
        }

        public bool value { get; set; }

        public override void SetValue(cEMI emi)
        {
            base.SetValue(emi);
            value = emi.Eis1;
        }

        public void SetValue(bool val)
        {
            value = val;
            time = DateTime.Now;
            rawValue = new byte[1];
            if (value)
                rawValue[0] = (byte)(rawValue[0] | ((byte)0x01));
            else
                rawValue[0] = (byte)(rawValue[0] & (byte)0xf0);

        }


        public override String ToString()
        {
            String erg = base.ToString() + "  EIS1 = " + value.ToString();
            return erg;
        }

    }
}
