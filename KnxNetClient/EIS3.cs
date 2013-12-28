using System;
using System.Collections.Generic;
using System.Text;

namespace Knx
{
    class EIS3 : HDKnx
    {

        public EIS3()
        {
        }

        public EIS3(cEMI emi) : base(emi)
        {
            value = emi.Eis3;
        }

        public DateTime value { get; set; }

        public override void SetValue(cEMI emi)
        {
            base.SetValue(emi);
            value = emi.Eis3;
        }


        public  void SetValue(DateTime time)
        {
            value = time;
            time = DateTime.Now;

            rawValue = new byte[4];
            rawValue[0] = 0;
            rawValue[1] = (byte)value.Hour;
            rawValue[2] = (byte)value.Minute;
            rawValue[3] = (byte)value.Second;

        }


        public override String ToString()
        {
            String erg = base.ToString() + "  EIS3 = " + value.ToString();
            return erg;
        }

    }
}
