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

namespace Knx
{
    public partial class KnxNetForm : Form
    {
        KnxNetConnection KnxCon = new KnxNetConnection();
        //static public System.Windows.Forms.TextBox tb_Log;
        delegate void StringParameterWithStatusDelegate(string Text);
        String Filename = "KNXLog.trx";
        String logFilename = "KNXNetClientLog.txt";
        const int msPerDay = 86400000;
        const int maxAnzLines=200;

        public KnxNetForm()
        {
            InitializeComponent();
            logFilename = "KNXNetClientLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Filename = calculateInitialFilename();
            HDKnxHandler.Load();
            KnxCon.SetLog(AddLogText);
            KnxCon.SetReceivedFunction(NewTelegramReceived);
            KnxCon.SetInfo(NewInfoReceived);
            //KnxCon.SetDataChangedFunction(DataChanged);
            KnxCon.SetRawReceivedFunction(NewRawTelegramReceived);
            SetDefaultGateway();
            timerFileName.Interval = msPerDay - (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            timerFileName.Start();
        }

        private string calculateInitialFilename()
        {
            DateTime now = DateTime.Now;
            String fn = "KnxLog_" + now.ToString("yyyyMMdd_HHmmss") + ".trx";
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
                    if (IP.StartsWith("192.168.0.")) cBGatewayIP.SelectedIndex = 0;
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
                AddToTextbox(Environment.NewLine + emi.ToString());
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
                String line =HomeData.KnxTools.BytesToTrxString(raw) + Environment.NewLine;
                System.IO.File.AppendAllText(Filename,line);
            }

        }


        private void NewInfoReceived(String txt)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new KnxNetConnection.SendInfoDelegate(NewInfoReceived), new object[] { txt });
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
                AddToTextbox(Environment.NewLine + "DC-->   " + hdKnx.ToString());
            }
        }


        private void AddToTextbox(String txt)
        {
            if (tBResponse.Lines.Length > maxAnzLines)
            {
                int selectionstart = tBResponse.SelectionStart;
                int firstlinelength = tBResponse.Lines[0].Length;
                tBResponse.Text = tBResponse.Text.Remove(0, firstlinelength + 2);
                tBResponse.SelectionStart = selectionstart - firstlinelength;
            }
            tBResponse.AppendText(txt);

        }





        // GUI

        private void Open_Click(object sender, EventArgs e)
        {
            String GatewayIp = (String) cBGatewayIP.SelectedItem;
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
            System.IO.File.AppendAllText(logFilename, Environment.NewLine + DateTime.Now.ToString("dd.MM.yy HH:mm:ss") + ": " + Text);
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
            cEMI emi = new cEMI(new EIB_Adress("0/1/56"), false);
            KnxCon.Send(emi);
        }

        private void btn_SendAus_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress("0/1/56"), true);
            //emi.APCI = APCI_Typ.Request;
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

    }
}
