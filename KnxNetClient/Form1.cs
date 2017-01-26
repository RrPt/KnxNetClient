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

        public KnxNetForm()
        {
            InitializeComponent();
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
            int msTo8 =  (int)(new DateTime(2016,1,1,8,0,0)).TimeOfDay.TotalMilliseconds - (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            if (msTo8 < 0) msTo8 += msPerDay;
            timerRollosRrPt.Interval = msTo8;
            timerRollosRrPt.Start();
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
                    if (hdKnx.emi != null)
                    {
                        AddToTextbox(Environment.NewLine + "DC:" + hdKnx.ToString() + "  emi=" + hdKnx.emi.ToString());
                    }
                    else
                    {
                        AddToTextbox(Environment.NewLine + hdKnx.ToString());
                    }
                }
                catch (Exception e)
                {

                    AddToTextbox(e.ToString());
                }
            }
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
            String GatewayIp = (String)cBGatewayIP.SelectedItem;
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



        private void bt_SendAn_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/0/56"), true);
            KnxCon.Send(emi);
        }

        private void btn_SendAus_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/0/56"), false);
            KnxCon.Send(emi);
        }


        private void bt_SendDimDunkler_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/0/57"), (byte)1);
            KnxCon.Send(emi);
        }

        private void btn_SendDimHeller_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/0/57"), (byte)9);
            KnxCon.Send(emi);
        }

        private void btn_SendDimStop_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/0/57"), (byte)0);
            KnxCon.Send(emi);
        }


        // Rollos
        private void bt_RolloAb_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/1/0"), true);
            KnxCon.Send(emi);
        }

        private void btn_RolloAuf_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/1/0"), false);
            KnxCon.Send(emi);
        }

        private void bt_LamelleAb_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/1/1"), true);
            KnxCon.Send(emi);
        }

        private void btn_LamelleAuf_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("1/1/1"), false);
            KnxCon.Send(emi);
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

        private void button14_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromEsfFile("ADS-TEC.esf");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromEsfFile("Petzoldt.esf");

        }

        private void btn_DimDunkler_MouseDown(object sender, MouseEventArgs e)
        {
            bt_SendDimDunkler_Click(sender, e);
        }

        private void btn_DimDunkler_MouseUp(object sender, MouseEventArgs e)
        {
            btn_SendDimStop_Click(sender, e);
        }

        private void btn_DimHeller_MouseDown(object sender, MouseEventArgs e)
        {
            btn_SendDimHeller_Click(sender, e);
        }

        private void btn_DimHeller_MouseUp(object sender, MouseEventArgs e)
        {
            btn_SendDimStop_Click(sender, e);
        }

        private void KnxNetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close_Click(sender, e);
        }

        private void btnRolloRrPtKorrektur_Click(object sender, EventArgs e)
        {
            rolloKorrektur();

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

        private void timerRollosRrPt_Tick(object sender, EventArgs e)
        {
            rolloKorrektur();
            sheddachRunter();

            AddToTextbox(Environment.NewLine + "Korrektur Rollo RrPt");

            timerRollosRrPt.Interval = msPerDay;
        }


    }
}
