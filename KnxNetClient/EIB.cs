using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace EIBDef
{
    enum EIB_Adress_Typ  {PhysAdr,GroupAdr};
    enum APCI_Typ { Request, Answer, Send, unnown };
    enum EIS_Typ { unknown, EIS1, EIS2, EIS3, EIS4, EIS5, EIS6, EIS7, EIS8, EIS9, EIS10, EIS11 };

    ///<summary >
    ///Definiert eine EIB-Bus Adresse
    ///</summary>
    class EIB_Adress
    {
        private const int MAX_ADR = 0xFFFF;                         // max. Adr.
        private const int MIN_ADR = 0x0;                            // min. Adr.
        private ushort m_Adr = 0;                                    // Adresse
        private EIB_Adress_Typ m_Typ  = EIB_Adress_Typ.GroupAdr;    // Art der Adresse

        // Konstruktor Gruppenadr. aus Integer anlegen
        public EIB_Adress(int GA)
        {
            if (GA > MAX_ADR) throw new Exception("Gruppenadresse zu gross");
            if (GA < MIN_ADR) throw new Exception("Gruppenadresse darf nicht negativ sein");
            m_Adr = (ushort)GA;
            m_Typ  = EIB_Adress_Typ.GroupAdr;
        }

        // Konstruktor Gruppenadr. oder Physik. Adr aus Integer anlegen
        public EIB_Adress(int Adr,EIB_Adress_Typ Typ)
        {
            if (Adr > MAX_ADR) throw new Exception("zu grosse Adresse");
            if (Adr < MIN_ADR) throw new Exception("Adresse darf nicht negativ sein");
            m_Adr = (ushort)Adr;
            m_Typ = Typ;
        }

        // Konstruktor Gruppenadr. aus Teiladressen erzeugen
        public EIB_Adress(ushort HG, ushort MG, ushort UG)
        {
            Set_GA(HG, MG, UG);
        }


        // MSB der Adr abfragen
        public byte MSB
        {
            get
            {
                return (byte)(m_Adr >> 8);
            }
        }

        // LSB der Adr abfragen
        public byte LSB
        {
            get
            {
                return (byte)(m_Adr & 0xFF);
            }
        }
        

        // Adresstyp abfragen
        public EIB_Adress_Typ Typ
        {
            get { return m_Typ; }
        }


        // Gruppenadr als ushort abfragen/setzen
        private ushort GA
        {
            get
            {
                if (m_Typ != EIB_Adress_Typ.GroupAdr) throw new Exception("keine Gruppenadresse");
                return m_Adr;
            }
            set
            {
                m_Adr = value;
                m_Typ  = EIB_Adress_Typ.GroupAdr;
            }
        }

        // Physik. Adr als ushort abfragen/setzen
        private ushort PA
        {
            get
            {
                if (m_Typ != EIB_Adress_Typ.PhysAdr) throw new Exception("keine physikalische Adresse");
                return m_Adr;
            }
            set
            {
                m_Adr = value;
                m_Typ = EIB_Adress_Typ.PhysAdr;
            }
        }

        // Hauptgruppe abfragen
        public ushort Hauptgruppe
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.GroupAdr)
                    return (ushort)((m_Adr & 0xF800) >> 11);
                else
                {
                    throw new Exception("keine Gruppenadresse");
                }
            }
        }

        // Mittelgruppe abfragen
        public ushort Mittelgruppe
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.GroupAdr)
                    return (ushort)((m_Adr & 0x700) >> 8);
                else
                    throw new Exception("keine Gruppenadresse");
            }
        }

        // Untergruppe abfragen
        public ushort Untergruppe
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.GroupAdr)
                    return (ushort)(m_Adr & 0xFF);
                else
                    throw new Exception("keine Gruppenadresse");
            }
        }

        // Bereich abfragen
        public ushort Bereich
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.PhysAdr)
                    return (ushort)((m_Adr & 0xF000) >> 12);
                else
                    throw new Exception("keine physikalische Adresse");
            }
        }

        // Linie abfragen
        public ushort Linie
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.PhysAdr)
                    return (ushort)((m_Adr & 0xF00) >> 8);
                else
                    throw new Exception("keine physikalische Adresse");
            }
        }

        // Teilnehmer abfragen
        public ushort Teilnehmer
        {
            get
            {
                if (m_Typ == EIB_Adress_Typ.PhysAdr)
                    return (ushort)(m_Adr & 0xFF);
                else
                    throw new Exception("keine physikalische Adresse");
            }
        }


        // Gruppenadr. setzen
        private ushort Set_GA(ushort HG, ushort MG, ushort UG)
        {
            if (UG > 255) throw new Exception("Untergruppe zu gross");
            if (MG > 7) throw new Exception("Untergruppe zu gross");
            if (HG > 31) throw new Exception("Untergruppe zu gross");

            m_Adr = (ushort)((HG << 11) + (MG << 8) + UG);
            m_Typ = EIB_Adress_Typ.GroupAdr;
            return m_Adr;
        }

        // Physikadr. setzen
        private ushort Set_PA(ushort Bereich, ushort Linie, ushort Teilnehmer)
        {
            if (Teilnehmer > 255) throw new Exception("Teilnehmer zu gross");
            if (Linie > 15) throw new Exception("Linie zu gross");
            if (Bereich > 15) throw new Exception("Bereich zu gross");

            m_Adr = (ushort)(( Bereich << 12) + (Linie << 8) + Teilnehmer);
            m_Typ = EIB_Adress_Typ.PhysAdr;
            return m_Adr;
        }

        // Ausgabe als String
        public override string ToString()
        {
            if (m_Typ == EIB_Adress_Typ.GroupAdr)
            {
                return Hauptgruppe + "/" + Mittelgruppe + "/" + Untergruppe;
            }
            else
            {
                return Bereich + "." + Linie + "." + Teilnehmer;
            }
        }


        // Vergleich zweier Adr.
        public bool Equals(EIB_Adress obj)
        {
            if (m_Typ != obj.m_Typ) return false;
            return m_Adr == obj.m_Adr;
        }
    }


    ///<summary>
    ///Definiert ein EIB-Bus Telegramm
    ///</summary>
    class EIB_Telegramm
    {
        private const int EIB_Phys_Source_Adr = 0x1280; // 1.2.128

        private EIB_Adress m_source;                    // physik. Absenderaddr
        private EIB_Adress m_destination;               // Zieladr
        private byte[] m_value ;                        // Inhalt des Telegramme (uninterpretiert)
        private DateTime m_ReceiveTime ;                // Zeit des anlegens
        private int m_DataLen;                          // Datenlänge
        private APCI_Typ m_APCI;                        // APCI-Typ

        // Konstruktor aus empfangenen Rohdaten
        public EIB_Telegramm(byte [] raw)
        {
            m_ReceiveTime = DateTime.Now;
//xxx            if (raw.Length < 10) throw new Exception("Rohdaten zu kurz");
 
            // Quelle eintragen
            m_source = new EIB_Adress((raw[1]<<8)+raw[2],EIB_Adress_Typ.PhysAdr);

            // Ziel eintragen
            //if ((raw[5]&0x80 ) == 0x80)
                m_destination = new EIB_Adress((raw[3] << 8) + raw[4], EIB_Adress_Typ.GroupAdr);
            //else
            //    m_destination = new EIB_Adress((raw[3] << 8) + raw[4], EIB_Adress_Typ.PhysAdr);

            // Datenlänge bestimmen
            m_DataLen = raw[5] & 0x0F;

            // APCI bestimmen
            m_APCI = (APCI_Typ)((raw[7]>>6) & 0x03);

            // Daten kopieren
            m_value = new byte[m_DataLen];
            for (ushort i = 0; i < m_DataLen; i++)
            {
                m_value[i] = raw[i + 7];
            }
            // APCI-Flag aus den Daten ausblenden
            if (m_DataLen>0) m_value[0] =(byte)( (byte)m_value[0] & (byte)0x3F);
        }

 


        // Konstruktor für EIS1 Telegramm: bool
        public EIB_Telegramm(EIB_Adress DestinationAdr,bool value, APCI_Typ apci )
        {
            m_ReceiveTime = DateTime.Now;
            m_source = new EIB_Adress(EIB_Phys_Source_Adr, EIB_Adress_Typ.PhysAdr);
            m_destination = DestinationAdr;
            m_APCI = apci;
            m_DataLen = 1;
            m_value = new byte[m_DataLen];
            if (value) m_value[0] = (byte)1;
            else m_value[0] = (byte)0;
        }


        // Konstruktor für EIS3 Telegramm: Zeit
        public EIB_Telegramm(EIB_Adress DestinationAdr, DateTime time, APCI_Typ apci)
        {
            m_ReceiveTime = DateTime.Now;
            m_source = new EIB_Adress(EIB_Phys_Source_Adr, EIB_Adress_Typ.PhysAdr);
            m_destination = DestinationAdr;
            m_APCI = apci;
            Eis3 = time;
        }



        // Konstruktor für EIS5: float
        public EIB_Telegramm(EIB_Adress DestinationAdr, float value, APCI_Typ apci)
        {
            m_ReceiveTime = DateTime.Now;
            m_source = new EIB_Adress(EIB_Phys_Source_Adr, EIB_Adress_Typ.PhysAdr);
            m_destination = DestinationAdr;
            m_APCI = apci;
            Eis5 = value;
        }


        // Quelladr abfragen
        public EIB_Adress SourceAdr
        {
            get
            {
                return m_source;
            }
        }


        // Zieladresse abfragen
        public EIB_Adress DestAdr
        {
            get
            {
                return m_destination;
            }
        }


        // Datenlänge abfragen
        public int DataLen
        {
            get
            {
                return m_DataLen;
            }
        }


        // APCI abfragen
        public byte apci_byte
        {
            get
            {
                return (byte)(((byte)m_APCI)<<6);
            }
        }

        // APCI abfragen
        public APCI_Typ apci
        {
            get
            {
                return m_APCI;
            }
        }


        // Rohdaten abfragen
        public byte[] value
        {
            get
            {
                return m_value;
            }
        }


        // Empfangszeit abfragen und setzen
        public DateTime ReceiveTime
        {
            get
            {
                return m_ReceiveTime;
            }
            set
            {
                m_ReceiveTime = value;
            }
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
                return m_value[0]==1;
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
                    erg = new DateTime(2000, 1,1, 0, 0, 0); 
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
                if ((m_value[1]&0x80)==0x80) m = (short)(m - 0x1000);

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

        // Ausgabe als String
        public override string ToString()
        {
            String erg = m_ReceiveTime + " [" + m_source.ToString().PadLeft(9) + "-->" + m_destination.ToString().PadRight(7) + " (" + m_APCI.ToString().PadLeft(7) + ")]: ";
            switch (m_DataLen)
            {
                case 1:  erg= erg + "  EIS1=" + Eis1.ToString().PadRight(7);
                         break;
                case 4:  erg= erg + "  EIS3=" + Eis3.ToString("H:m:s").PadRight(7);
                         erg= erg + "  EIS4=" + Eis4.ToString("d").PadRight(7);
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

        
    }


}  // namespace EIBDef
