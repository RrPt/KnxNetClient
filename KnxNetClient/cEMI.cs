using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using EIBDef;

namespace Knx
{


    [StructLayout(LayoutKind.Sequential,Pack=1)]
    class cEMI
    {
        //[FieldOffset(0)]
        public byte msgCode;

        //[FieldOffset(1)]
        public byte adInfoLen;

        //[FieldOffset(2)]
        public byte Control1;

        //[FieldOffset(3)]
        public byte control2;

        //[FieldOffset(4)]
        public byte sourceAdrH;

        //[FieldOffset(5)]
        public byte sourceAdrL;

        //[FieldOffset(6)]
        public byte DestAdrH;

        //[FieldOffset(7)]
        public byte DestAdrL;

        //[FieldOffset(8)]
        public byte dataLen;

        //[FieldOffset(9)]
        public short APDU;

        public byte byte1;
        public byte byte2;
        public byte byte3;
        public byte byte4;
        public byte byte5;

        public EIB_Adress m_source;                    // physik. Absenderaddr
        public EIB_Adress m_destination;               // Zieladr
        public byte[] m_value;                        // Inhalt des Telegramme (uninterpretiert)
        public DateTime m_ReceiveTime;                // Zeit des anlegens
        public int m_DataLen;                          // Datenlänge
        public APCI_Typ m_APCI;                        // APCI-Typ

        public cEMI()
        {
            m_value = new byte[15];
        }

        /// <summary>
        /// Kopiert Daten aus einem Byte-Array in eine cEMI Strukture (struct). Die Struktur muss ein sequenzeilles Layout besitzen. ( [StructLayout(LayoutKind.Sequential)] 
        /// </summary>
        /// <param name="array">Das Byte-Array das die daten enthält</param>
        /// <returns>cEMI object</returns>
        public static cEMI ByteToData(byte[] array)
        {

            //if (structType.StructLayoutAttribute.Value != LayoutKind.Sequential)
            //    throw new ArgumentException("structType ist keine Struktur oder nicht Sequentiell.");

            int size = Marshal.SizeOf(typeof(cEMI));
            //if (array.Length < (size))
            //    throw new ArgumentException("Byte-Array hat die falsche Länge.");

            byte[] tmp = new byte[size];
            Array.Copy(array, 0, tmp, 0, array.Length);

            GCHandle structHandle = GCHandle.Alloc(tmp, GCHandleType.Pinned);
            cEMI emi = (cEMI)Marshal.PtrToStructure(structHandle.AddrOfPinnedObject(), typeof(cEMI));
            structHandle.Free();
            emi.m_ReceiveTime = DateTime.Now;

            // Quelle eintragen
            emi.m_source = new EIB_Adress((emi.sourceAdrH << 8) + emi.sourceAdrL,EIB_Adress_Typ.PhysAdr);
            // Ziel eintragen
            if ((emi.control2&0x80 ) == 0x80)
                emi.m_destination = new EIB_Adress((emi.DestAdrH << 8) + emi.DestAdrL, EIB_Adress_Typ.GroupAdr);
            else
                emi.m_destination = new EIB_Adress((emi.DestAdrH << 8) + emi.DestAdrL, EIB_Adress_Typ.PhysAdr);

            emi.m_DataLen = emi.dataLen;

            // APCI bestimmen
            emi.m_APCI = (APCI_Typ)((array[10] >> 6) & 0x03);

            // Daten kopieren
            emi.m_value = new byte[emi.m_DataLen];
            for (ushort i = 0; i < emi.m_DataLen; i++)
            {
                emi.m_value[i] = array[i + 10];
            }
            // APCI-Flag aus den Daten ausblenden
            if (emi.m_DataLen > 0) emi.m_value[0] = (byte)((byte)emi.m_value[0] & (byte)0x3F);


            return emi;
        }


        public byte[] DataToByte()
        {

            //if (structType.StructLayoutAttribute.Value != LayoutKind.Sequential)
            //    throw new ArgumentException("structType ist keine Struktur oder nicht Sequentiell.");

            int size = m_DataLen+10;

            byte[] tmp = new byte[size];
            tmp[0] = 0x11;
            tmp[1] = 0x00;
            tmp[2] = 0xBC;
            tmp[3] = 0xE0;
            tmp[4] = m_source.MSB;
            tmp[5] = m_source.LSB;
            tmp[6] = m_destination.MSB;
            tmp[7] = m_destination.LSB;
            tmp[8] = (byte)m_DataLen;
            tmp[9] = 0x00;
            if (m_DataLen == 1)
            {
            tmp[10] = (byte)(0x80 | m_value[0]);
            }
            // xxx für andere Datenlängen ergänzen
            return tmp;
        }

        // Ausgabe als String
        public override string ToString()
        {
            String erg = m_ReceiveTime + " [" + m_source.ToString().PadLeft(9) + "-->" + m_destination.ToString().PadRight(7) + " (" + m_APCI.ToString().PadLeft(7) + ")]: ";
            switch (m_DataLen)
            {
                case 1: erg = erg + "  EIS1=" + Eis1.ToString().PadRight(7);
                    break;
                case 4: erg = erg + "  EIS3=" + Eis3.ToString("H:m:s").PadRight(7);
                    erg = erg + "  EIS4=" + Eis4.ToString("d").PadRight(7);
                    break;
                case 5: erg = erg + "  EIS11=" + Eis11.ToString().PadRight(6);
                    break;
                case 3: erg = erg + "  EIS5=" + Eis5.ToString().PadRight(7);
                    break;
                default:
                    break;
            }
            erg = erg + DataToRaw();
            return erg;
        }

        // Ausgabe der Rohdaten als String
        private String DataToRaw()
        {
            String erg = "";
            for (ushort i = 0; i < m_DataLen; i++)
            {
                erg += m_value[i].ToString() + " ";
            }
            return erg;
        }

        // Abfrage der Daten in EIS1-Darstellung (bool)
        public bool Eis1
        {
            get
            {
                if (m_DataLen != 1)
                {  // keine EIS1
                    //throw new Exception("Kein EIS1-Datenformat");
                    //L.err("Kein EIS1-Datenformat");
                    return false;
                }
                return m_value[0] == 1;
            }
            //Set Fehlt
            set
            {
                if (value)
                    m_value[0] = (byte)( m_value[0] | ((byte)0x01));
                else
                    m_value[0] = (byte)( m_value[0] & (byte)0xf0);
            }
        }

        // Abfrage der Daten in  EIS3-Darstellung (Zeit -> DateTime)
        public DateTime Eis3
        {
            get
            {
                if (m_DataLen != 4)
                {   // keine EIS3
                    throw new Exception("Kein EIS3-Datenformat");
                }
                if (m_value[1] > 24) m_value[1] = 0;
                if (m_value[2] > 59) m_value[2] = 0;
                if (m_value[3] > 59) m_value[3] = 0;
                DateTime erg = new DateTime(2000, 1, 1, m_value[1] & 0x1F, m_value[2], m_value[3]);
                return erg;
            }
            set
            {
                m_DataLen = 4;
                m_value = new byte[4];
                m_value[0] = 0;
                m_value[1] = 10;
                m_value[2] = 20;
                m_value[3] = 30;
                m_value[1] = (byte)value.Hour;
                m_value[2] = (byte)value.Minute;
                m_value[3] = (byte)value.Second;
            }
        }

        // Abfrage der Daten in  EIS4-Darstellung (Datum -> DateTime)
        public DateTime Eis4
        {
            get
            {
                DateTime erg;
                if (m_DataLen != 4)
                {   // keine EIS4
                    throw new Exception("Kein EIS4-Datenformat");
                }

                try
                {
                    if (m_value[2] > 12) m_value[2] = 1;
                    if (m_value[1] > 31) m_value[1] = 1;
                    if (m_value[2] < 1) m_value[2] = 1;
                    if (m_value[1] < 1) m_value[1] = 1;
                    erg = new DateTime(m_value[3] + 2000, m_value[2], m_value[1], 0, 0, 0);

                }
                catch (Exception)
                {
                    erg = new DateTime(2000, 1, 1, 0, 0, 0);
                }
                return erg;
            }
        }




        // Umwandlung der EIS5-Darstellung in eine Floatzahl und umgekehrt
        public float Eis5
        {
            get
            {
                if (m_DataLen != 3) return 0.0f;         // keine EIS5
                int inv = m_value[1] * 256 + m_value[2];
                short e, m;

                e = (short)((m_value[1] >> 3) & 0x0f);
                m = (short)(((m_value[1] & 0x07) + ((m_value[1] >> 4) & 0x8)) * 256 + m_value[2]);
                if ((m_value[1] & 0x80) == 0x80) m = (short)(m - 0x1000);

                return (0.01f * m * (1 << e));
            }
            set
            {
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

                m_DataLen = 3;
                m_value = new byte[m_DataLen];

                short ival = (short)((e << 11) + (m & 0x7ff) + ((m & 0x800) << 4));
                m_value[0] = 0;
                m_value[1] = (byte)(ival >> 8);
                m_value[2] = (byte)(ival & 0xFF);

            }

        }


        // Umwandlung der EIS11-Darstellung in eine uint und umgekehrt
        public uint Eis11
        {
            get
            {
                if (m_DataLen != 5) return 0;         // keine EIS11
                return (uint)(m_value[1] * (1 << 24) + m_value[2] * (1 << 16) + m_value[3] * (1 << 8) + m_value[4]);
            }
        }


        // Pruft ob das Telegramm dem EIS-Typ entsprechen kann
        public bool IsEisTyp(EIS_Typ type)
        {
            switch (type)
            {
                case EIS_Typ.EIS1:
                    return m_DataLen == 1;
                case EIS_Typ.EIS2:
                    return m_DataLen == 1;
                case EIS_Typ.EIS3:
                    return m_DataLen == 4;
                case EIS_Typ.EIS4:
                    return m_DataLen == 4;
                case EIS_Typ.EIS5:
                    return m_DataLen == 3;
                case EIS_Typ.EIS11:
                    return m_DataLen == 5;
                default:
                    return false;
            }
        }


    }

}
