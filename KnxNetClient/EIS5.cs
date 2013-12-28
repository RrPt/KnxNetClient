using System;
using System.Collections.Generic;
using System.Text;

namespace Knx
{
    class EIS5 : HDKnx
    {
        public EIS5()
        {
        }

        public EIS5(cEMI emi) : base(emi)
        {
            value = emi.Eis5;
        }

        public EIS5(string name, EIBDef.EIB_Adress adr)
        {
            // TODO: Complete member initialization
            this.name = name;
            destAdr = adr;
            sourceAdr = new EIBDef.EIB_Adress(0);
            value = 0f;
        }

        public float value { get; set; }


        public override  void SetValue(cEMI emi) 
        {
            base.SetValue(emi);
            value = emi.Eis5;
        }


        public void SetValue(float val)
        {
            value = val;
            time = DateTime.Now;

            short e, m;
            float rnd;

            if (value < 0) rnd = -0.5f;
            else rnd = 0.5f;

            if (Math.Abs(value) < 20.0f)
            {  // kein exponent
                e = 0;
                m = (short)(value * 100 + rnd);
            }
            else
            {  // mit exponent
                e = (short)(Math.Floor(1 + Math.Log(Math.Abs(value / 20.0)) / Math.Log(2.0)));
                if (e > 15) e = 15;
                m = (short)(100.0f * value / (1 << e) + rnd);
                if (m > 2047) m = 2047;
                if (m < -2048) m = -2048;
            }

            rawValue = new byte[3];

            short ival = (short)((e << 11) + (m & 0x7ff) + ((m & 0x800) << 4));
            rawValue[0] = 0;
            rawValue[1] = (byte)(ival >> 8);
            rawValue[2] = (byte)(ival & 0xFF);



        }

        public override String ToString()
        {
            String erg = base.ToString() + "  EIS5 = " + value.ToString();
            return erg;
        }

    }
}
