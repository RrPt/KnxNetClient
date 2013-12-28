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

namespace Knx
{
    public partial class KnxNetForm : Form
    {
        KnxNetConnection KnxCon = new KnxNetConnection();
        //static public System.Windows.Forms.TextBox tb_Log;
        delegate void StringParameterWithStatusDelegate(string Text);


        public KnxNetForm()
        {
            InitializeComponent();
            HDKnxHandler.Load();
            timer1.Start();
  //        KnxCon.SetLog(AddLogText);
  //        KnxCon.SetReceivedFunction(NewTelegramReceived,this);
            KnxCon.SetDataChangedFunction(DataChanged, this);

        }


        private void NewTelegramReceived(cEMI emi)
        {
            tBResponse.AppendText(Environment.NewLine + "    " +emi.ToString());
        }

        private void DataChanged(HDKnx hdKnx)
        {
            tBResponse.AppendText(Environment.NewLine + hdKnx.ToString());
        }



        private void GetData()
        {
            cEMI emi = KnxCon.GetData();
            if (emi != null) tBResponse.AppendText(Environment.NewLine + emi.ToString()); // KnxTools.BytesToString(tele));
            else tBResponse.AppendText(Environment.NewLine + "no Telegramm");
        }


        // GUI

        private void Open_Click(object sender, EventArgs e)
        {
            String GatewayIp = "192.168.0.3";
            bool ok = KnxCon.Open(GatewayIp);
            if (!ok) tBResponse.AppendText(Environment.NewLine + "Konnte keine Verbindung zum Gateway " + GatewayIp + " herstellen");
            else tBResponse.AppendText(Environment.NewLine + "Connected mit Gateway " + GatewayIp + "  ChannelId=" + KnxCon.channelId  );

        }

        private void Close_Click(object sender, EventArgs e)
        {
            bool ok = KnxCon.Close();
            if (!ok) tBResponse.AppendText(Environment.NewLine + "Verbindung zum Gateway konnte nicht getrennt werden ");
            else tBResponse.AppendText(Environment.NewLine + "Gateway disconnected");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetData();
        }


        private void btn_Heartbeat_Click(object sender, EventArgs e)
        {
            KnxCon.Heartbeat();
        }


        public void AddLogText(String Text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterWithStatusDelegate(AddLogText), new object[] { Text });
            }
            else
            {
                tBResponse.AppendText(Environment.NewLine + "                          ---  " + Text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           cEMI emi = KnxCon.GetData();
            while (emi != null)
            {
                //if (tele[2] == 0x02)
                //{   // Controltelegramm
                //    tBResponse.AppendText(Environment.NewLine + "<C:" + KnxTools.BytesToString(tele));
                //}
                //else
                {   // Tunneltelegramm
                    tBResponse.AppendText( Environment.NewLine + "Data:" + emi.ToString()); // KnxTools.BytesToString(tele));
                }

                emi = KnxCon.GetData();
            }
        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress(64), 11.1f);
            KnxCon.Send(emi);
        }

        private void btn_Send0_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(new EIB_Adress(64), true);
            emi.APCI = APCI_Typ.Request;
            KnxCon.Send(emi);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HDKnxHandler.WriteParametersToFile("KnxClientW.xml");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HDKnxHandler.ReadParametersFromFile("KnxClient.xml");
        }

    }
}
