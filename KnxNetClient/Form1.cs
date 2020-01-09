using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using EIBDef;
using HomeData;
using System.IO;
using System.Threading;
using DimmerSteuerelement;

namespace Knx
{
    public partial class KnxNetForm : Form
    {
        private static object threadlock = new object();
        KnxNetConnection KnxCon = new KnxNetConnection();
        //static public System.Windows.Forms.TextBox tb_Log;
        delegate void StringParameterWithStatusDelegate(string Text);
        String Filename = "log\\KNXLog.trx";
        String logFilename = "log\\KNXNetClientLog.txt";
        String debugFilename = "log\\KNXNetClientDebug.txt";
        const int msPerDay = 86400000;
        const int maxAnzLines = 200;
        private ConfigListConfig selectedConfig;
        float temperatur=-999;
        float aussenHelligkeit = -999;
        float AussenHelligkeitLast = -999;
        float helligkeitSued = -999;
        float helligkeitOst = -999;
        float helligkeitWest = -999;
        SortedList<string, DimmerControl> dimmerList = new SortedList<string, DimmerControl>();

        public KnxNetForm()
        {
            InitializeComponent();

            ConfigList configList = ConfigIO.read("KnxNetClient_Configs.xml");
            // bestimmen der Default Config
            selectedConfig = configList?.Config?[0];
            foreach (var item in configList?.Config)
            {
                if (configList?.defaultConfig == item?.name) selectedConfig = item;
            }

            SetControls();
            tabControl1.SelectTab(1);

            Directory.CreateDirectory("log");
            logFilename = "log\\KNXNetClientLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            debugFilename = "log\\KNXNetClientDebug_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Filename = calculateInitialFilename();
            HDKnxHandler.Load();
            KnxCon.SetErrFunction(AddLogText);
            KnxCon.SetDebugFunction(AddDebugText);
            //KnxCon.SetReceivedFunction(NewTelegramReceived);
            KnxCon.SetInfoFunction(NewInfoReceived);
            KnxCon.SetDataChangedFunction(DataChanged);
            KnxCon.SetRawReceivedFunction(NewRawTelegramReceived);
            tBHBIntervall_TextChanged(null, null);
            SetDefaultGateway();
            // bestimmen ms bis Mitternacht
            timerFileName.Interval = msPerDay - (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            timerFileName.Start();
            // bestimmen wieviel ms bis um 8:00 Uhr
            int msTo8 = (int)(new DateTime(2016, 1, 1, 8, 0, 0)).TimeOfDay.TotalMilliseconds - (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            if (msTo8 < 0) msTo8 += msPerDay;
            timerRollosRrPt.Interval = msTo8;
            timerRollosRrPt.Start();
            tBTemperatur.Text = temperatur + "°C";
            timerMain.Start();
        }

        private void SetControls()
        {
            // Controls für die Lichtsteuerung setzen
            LichtControl lc;
            int idx = 0;
            if (selectedConfig.LightList!=null)
            {
                foreach (var light in selectedConfig.LightList)
                {
                    lc = new LichtControl();
                    lc.Titel = light.name;
                    lc.EibAdress_IO = light.EibAdress_IO;
                    lc.EibAdress_Dimm = light.EibAdress_Dimm;
                    lc.SetKnxSendFunction(KnxCon.Send);
                    this.tpSteuerung.Controls.Add(lc);
                    lc.Location = new System.Drawing.Point(20, 100 + 120 * idx++);
                } 
            }

            // Controls für die Rollosteuerung setzen
            RolloControl rc;
            idx = 0;
            if (selectedConfig.RolloList != null)
            {
                foreach (var light in selectedConfig.RolloList)
                {
                    rc = new RolloControl();
                    rc.Titel = light.name;
                    rc.EibAdress_AufAb = light.EibAdress_AufAb;
                    rc.EibAdress_Lamelle = light.EibAdress_Lamelle;
                    rc.SetKnxSendFunction(KnxCon.Send);
                    this.tpSteuerung.Controls.Add(rc);
                    rc.Location = new System.Drawing.Point(420, 100 + 120 * idx++);
                }
            }

            // Controls für die LichtHellsteuerung setzen
            DimmerControl dse ;
            idx = 0;
            if (selectedConfig.LightHellList != null)
            {
                foreach (var light in selectedConfig?.LightHellList)
                {
                    dse = new DimmerControl();
                    dimmerList.Add(light.EibAdress_Hell, dse);
                    dse.Dimmer_Text = light.name;
                    if (light.EibAdress_Hell.EndsWith("156")) dse.DimmerHandleBefehl += HandleDimmerControl156;
                    if (light.EibAdress_Hell.EndsWith("157")) dse.DimmerHandleBefehl += HandleDimmerControl157;
                    this.tpSteuerung.Controls.Add(dse);
                    dse.Location = new System.Drawing.Point(720 + 120 * idx++, 100);
                }
            }

            int height = this.Size.Height;
            if (height < 200 + 120 * idx) height = 200 + 120 * idx;
            if (height < 550) height = 550;
            this.Size = new Size(this.Size.Width, height);



            // Markisen TAb Control setzen
            rC_Markise.Titel = "Markisensteuerung";
            rC_Markise.EibAdress_AufAb = selectedConfig.RolloList[1].EibAdress_AufAb;
            rC_Markise.EibAdress_Lamelle = selectedConfig.RolloList[1].EibAdress_Lamelle;
            rC_Markise.SetKnxSendFunction(KnxCon.Send);


        }

        public void HandleDimmerControl156(object sender, EventArgs e)
        {
            DimmerBefehl dimBef = (DimmerBefehl)sender;
            EIB_Adress eibHellTele = new EIB_Adress(1,0,156);
            HandleDimmerControl(dimBef, eibHellTele);
        }

        public void HandleDimmerControl157(object sender, EventArgs e)
        {
            DimmerBefehl dimBef = (DimmerBefehl)sender;
            EIB_Adress eibHellTele = new EIB_Adress(1, 0, 157);
            HandleDimmerControl(dimBef, eibHellTele);
        }

        public delegate void DimmerBefehlEventHandler(DimmerBefehl dimBef, EIB_Adress eibHellAdr);
        public void HandleDimmerControl(DimmerBefehl dimBef, EIB_Adress eibHellAdr)
        { 
            EIB_Telegramm tele = null;
            byte value = 0;

            if (InvokeRequired)
            {
                BeginInvoke(new DimmerBefehlEventHandler(HandleDimmerControl), new object[] { dimBef, eibHellAdr });
            }
            else
            {


                switch (dimBef.m_cmd)
                {
                    //case CmdList.Ein:
                    //    tele = new EIB_Telegramm(m_GA[Schalten], true, APCI_Typ.Send);
                    //    break;
                    //case CmdList.Aus:
                    //    tele = new EIB_Telegramm(m_GA[Schalten], false, APCI_Typ.Send);
                    //    break;
                    //case CmdList.Heller:
                    //    value = 9;
                    //    tele = new EIB_Telegramm(m_GA[Dimmen], value, APCI_Typ.Send);
                    //    break;
                    //case CmdList.Dunkler:
                    //    value = 1;
                    //    tele = new EIB_Telegramm(m_GA[Dimmen], value, APCI_Typ.Send);
                    //    break;
                    //case CmdList.Stop:
                    //    value = 0;
                    //    tele = new EIB_Telegramm(m_GA[Dimmen], value, APCI_Typ.Send);
                    //    break;
                    case CmdList.SetWert:
                        value = dimBef.m_value;
                        EIB_Adress destAdr = eibHellAdr;

                        tele = new EIB_Telegramm(destAdr, value, APCI_Typ.Send);
                        tele.Eis6 = value;
                        break;
                    default:
                        break;
                }
                if (tele != null) KnxCon.Send(new cEMI(tele));
            }
        }



        private string calculateInitialFilename()
        {
            DateTime now = DateTime.Now;
            String fn = "log\\KnxLog_" + now.ToString("yyyyMMdd_HHmmss") + ".trx";
            return fn;
        }


        private void SetDefaultGateway()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                if (addr[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    String IP = addr[i].ToString();
                    if (IP.StartsWith("192.168.254.")) cBGatewayIP.SelectedIndex = 1;
                    if (IP.StartsWith("10.")) cBGatewayIP.SelectedIndex = 1;
                    if (IP.StartsWith("192.168.22.")) cBGatewayIP.SelectedIndex = 0;
                }
            }

        }


        private void NewTelegramReceived(cEMI emi)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new KnxNetConnection.TelegramReceivedDelegate(NewTelegramReceived), new object[] { emi });
            }
            else
            {
                AddToTextbox(Environment.NewLine + "NT:"+ emi.ToString());
            }
        }


        private void NewRawTelegramReceived(byte[] raw)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new KnxNetConnection.RawTelegramReceivedDelegate(NewRawTelegramReceived), new object[] { raw });
            }
            else
            {
                //byte[] t = new byte[raw.Length];
                //Array.Copy(raw, 0, t, 0, raw.Length);
                String line = HomeData.KnxTools.BytesToTrxString(raw) + Environment.NewLine;
                System.IO.File.AppendAllText(Filename, line);
            }

        }


        private void NewInfoReceived(String txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new KnxNetConnection.MsgDelegate(NewInfoReceived), new object[] { txt });
            }
            else
            {
                AddToTextbox(Environment.NewLine + txt);
            }
        }

        private void toFile(float wert, string fn)
        {
            String line = String.Format("{0} ; {1}\n", DateTime.Now.ToString(), wert.ToString().Replace(".", ","));
            Console.WriteLine(line);
            File.AppendAllText(fn, line);
        }

        private void toFile(string wert, string fn)
        {
            String line = String.Format("{0} ; {1}\n", DateTime.Now.ToString(), wert);
            Console.WriteLine(line);
            File.AppendAllText(fn, line);
        }


        private void DataChanged(HDKnx hdKnx)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new KnxNetConnection.DataChangedDelegate(DataChanged), new object[] { hdKnx });
            }
            else
            {
                try
                {
                    AddToTextbox(Environment.NewLine + hdKnx.ToString());

                    // Licht Büro RrPt

                    if (dimmerList.ContainsKey(hdKnx.destAdr.ToString()))
                    {
                        DimmerControl dc = dimmerList[hdKnx.destAdr.ToString()];
                        byte hell = hdKnx.emi.Eis6;
                        dc.Dimmer_IstWert = hell;
                    }

                    // Temperatur
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 24).Adr)
                    {
                        temperatur = hdKnx.emi.Eis5;
                        tBTemperatur.Text = temperatur + "°C";
                        toFile(temperatur, "TemperaturData.txt");
                    }
                    // Wind
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 23).Adr)
                    {
                        float wind = hdKnx.emi.Eis5;
                        tbWind.Text = wind + "m/s";
                        toFile(wind, "WindData.txt");

                    }
                    // Aussenhelligkeit 
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 10).Adr)
                    {
                        aussenHelligkeit = hdKnx.emi.Eis5;
                        lblAussenHelligkeit.Text = aussenHelligkeit + "lux";
                        float HellSchwelle = 700;
                        if ((AussenHelligkeitLast>HellSchwelle) && (aussenHelligkeit<=HellSchwelle))
                            {   // nur vor um 19 Uhr
                            if (now.Hour<17)  SetLichtRrPt(true);
                        }
                        AussenHelligkeitLast = aussenHelligkeit;
                    }
                    // Helligkeit Süd
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 20).Adr)
                    {
                        helligkeitSued = hdKnx.emi.Eis5;
                        lblHelligkeitSued.Text = helligkeitSued + "lux";
                        toFile(helligkeitSued, "HellSuedData.txt");
                    }
                    // Helligkeit Ost
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 21).Adr)
                    {
                        helligkeitOst = hdKnx.emi.Eis5;
                        lblHelligkeitOst.Text = helligkeitOst + "lux";
                        toFile(helligkeitOst, "HellOstData.txt");
                    }
                    // Helligkeit West
                    if (hdKnx.destAdr.Adr == new EIB_Adress(0, 1, 22).Adr)
                    {
                        helligkeitWest = hdKnx.emi.Eis5;
                        lblHelligkeitWest.Text = helligkeitWest + "lux";
                        toFile(helligkeitWest, "HellWestData.txt");
                    }
                    // Debugmeldungen
                    if (hdKnx.destAdr.Hauptgruppe == 0 && hdKnx.destAdr.Mittelgruppe == 7)
                    {
                        string msg = String.Format("GA={0} wert={1}", hdKnx.destAdr.ToString(), hdKnx.emi.Eis1);
                        toFile(msg, "Debug.txt");
                    }
                }
                catch (Exception e)
                {

                    AddToTextbox(e.ToString());
                }
            }
        }

        private void SetLichtRrPt(bool zustand)
        {
            // Licht Büro RrPt ein
            cEMI emi = new cEMI(new EIB_Adress("1/0/56"), zustand);
            KnxCon.Send(emi);

            //Thread.Sleep(300);
            //// dunkler
            //emi = new cEMI(new EIB_Adress("1/0/57"), (byte)9);
            //KnxCon.Send(emi);

            //Thread.Sleep(300);
            //// Stop
            //emi = new cEMI(new EIB_Adress("1/0/57"), (byte)0);
            //KnxCon.Send(emi);
        }

        private void AddToTextbox(String txt)
        {
            if (tBResponse.Lines.Length > maxAnzLines)
            {
                int selectionstart = tBResponse.SelectionStart;
                int firstlinelength = tBResponse.Lines[0].Length;
                tBResponse.Text = tBResponse.Text.Remove(0, firstlinelength + 2);
                if (selectionstart - firstlinelength > 0) tBResponse.SelectionStart = selectionstart - firstlinelength;
            }
            tBResponse.AppendText(txt);

        }





        // GUI

        private void Open_Click(object sender, EventArgs e)
        {
            String GatewayIp = (String)cBGatewayIP.Text;
            bool ok = KnxCon.Open(GatewayIp);
            if (!ok) AddToTextbox(Environment.NewLine + "Konnte keine Verbindung zum Gateway " + GatewayIp + " herstellen");
            else AddToTextbox(Environment.NewLine + "Connected mit Gateway " + GatewayIp + "  ChannelId=" + KnxCon.channelId);

        }

        private void Close_Click(object sender, EventArgs e)
        {
            bool ok = KnxCon.Close();
            if (!ok) AddToTextbox(Environment.NewLine + "Verbindung zum Gateway konnte nicht getrennt werden ");
            else AddToTextbox(Environment.NewLine + "Gateway disconnected");
        }



        public void AddLogText(String Text)
        {
            // Schreibe in Textbox
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new StringParameterWithStatusDelegate(AddLogText), new object[] { Text });
            //}
            //else
            //{
            //    AddToTextbox(Environment.NewLine + "LOG:      ---  " + Text);
            //}

            // schreibe in Logfile
            lock (threadlock)
            {
                System.IO.File.AppendAllText(logFilename, Environment.NewLine + DateTime.Now.ToString("dd.MM.yy HH:mm:ss") + ": " + Text);
            }
        }


        public void AddDebugText(String Text)
        {
            // Schreibe in Textbox
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new StringParameterWithStatusDelegate(AddLogText), new object[] { Text });
            //}
            //else
            //{
            //    AddToTextbox(Environment.NewLine + "LOG:      ---  " + Text);
            //}

            // schreibe in Logfile
            lock (threadlock)
            {
                System.IO.File.AppendAllText(debugFilename, Environment.NewLine + DateTime.Now.ToString("dd.MM.yy HH:mm:ss") + ": " + Text);
            }
        }



        public void AddInfoText(String Text)
        {
            // Schreibe in Textbox
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterWithStatusDelegate(AddLogText), new object[] { Text });
            }
            else
            {
                AddToTextbox(Environment.NewLine + "LOG:      ---  " + Text);
            }
        }


        private void button_WriteXML_Click(object sender, EventArgs e)
        {
            HDKnxHandler.WriteParametersToFile("KnxClientW.xml");
        }

        private void button_ReadXML_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromFile("KnxClient.xml");
        }

        private void timerFileName_Tick(object sender, EventArgs e)
        {
            Filename = calculateInitialFilename();

            AddToTextbox(Environment.NewLine + "Neuer Filename = " + Filename);

            timerFileName.Interval = msPerDay;

        }

        private void tBHBIntervall_TextChanged(object sender, EventArgs e)
        {
            KnxCon.HeartbeatInterval = int.Parse(tBHBIntervall.Text);
        }


        private void KnxNetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close_Click(sender, e);
        }


        private void rolloKorrektur()
        {
            cEMI emi = new cEMI(new EIB_Adress("1/1/0"), false);
            KnxCon.Send(emi);
            Thread.Sleep(30000);
            emi.Eis1 = true;
            emi = new cEMI(new EIB_Adress("1/1/1"), true);
            for (int i = 0; i < 7; i++)
            {
                KnxCon.Send(emi);
                Thread.Sleep(400);
            }
        }


        private void sheddachRunter()
        {
            cEMI emi = new cEMI(new EIB_Adress("6/1/2"), true);
            KnxCon.Send(emi);
        }

        // wird nur früh um 8 aufgerufen, die GUI-Funktion macht das Control
        private void timerRollosRrPt_Tick(object sender, EventArgs e)
        {
            Thread rolloThread;
            rolloThread = new Thread(new ThreadStart(rolloKorrektur));
            rolloThread.Start();

            //sheddachRunter();

            AddToTextbox(Environment.NewLine + "Korrektur Rollo RrPt");

            timerRollosRrPt.Interval = msPerDay;
        }

        private void btn_load_ESF_ads_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromEsfFile("ADS-TEC.esf");
        }

        private void btn_load_ESF_petz_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromEsfFile("Petzoldt.esf");
        }

        private void KnxNetForm_Shown(object sender, EventArgs e)
        {
            Open_Click(null, null);
        }

        private void KnxNetForm_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("KnxNetClient V{0}", Program.versionStr);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            EIB_Telegramm tele = null;

            tele = new EIB_Telegramm(new EIB_Adress(0, 1, 0), false, APCI_Typ.Send);
            if (tele != null) KnxCon.Send(new cEMI(tele));

            tele = new EIB_Telegramm(new EIB_Adress(0, 1, 200), true, APCI_Typ.Send);
            if (sender != null) KnxCon.Send(new cEMI(tele));

            tele = new EIB_Telegramm(new EIB_Adress(0, 1, 6), false, APCI_Typ.Send);
            if (tele != null) KnxCon.Send(new cEMI(tele));

            tele = new EIB_Telegramm(new EIB_Adress(6, 1, 2), true, APCI_Typ.Send);
            if (sender != null) KnxCon.Send(new cEMI(tele));

        }

        DateTime lastTick = DateTime.MinValue;
        DateTime now = DateTime.MinValue;
        private void timerMain_Tick(object sender, EventArgs e)
        {
            now = DateTime.Now;

            if (IsTime(18,00))
            {   // 18 Uhr licht aus
                SetLichtRrPt(false);
            }
            if ((now.Second == 0) && (cBSendZyklNoWind.Checked))
            {
                AddToTextbox(Environment.NewLine + "Telegramm kein Wind senden:");

                btnTest_Click(null, null);
                Console.WriteLine("Kein Wind gesendet: {0}  {1}", now, lastTick);
            }

            //Console.WriteLine("{0}  {1}",now,lastTick);
            lastTick = now;
        }

        private bool IsTime(int h, int min)
        {
            if (h != now.Hour) return false;            // falsche Stunden
            if (min != now.Minute) return false;        // falsche Minute
            if (min == lastTick.Minute) return false;   // richtige Minute aber Tick war schon
            return true;
        }
    }
}
