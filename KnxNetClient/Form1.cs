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
            timer1.Start();
            //tb_Log = tBResponse;
            KnxCon.SetLog(AddLogText);


        }

 
        private void GetData()
        {
            byte[] tele = KnxCon.GetData();
            if (tele != null) tBResponse.AppendText(Environment.NewLine + KnxTools.BytesToString(tele));
            else tBResponse.AppendText(Environment.NewLine + "no Telegramm");
        }


        // GUI

        private void Open_Click(object sender, EventArgs e)
        {

            ////Test
            //cEMI emi = new cEMI();

            //byte[] arr = new byte[5]; 
            //arr[0] = 5;
            //arr[1] = 10;
            //arr[2] = 20;
            //arr[3] = 30;
            //arr[4] = 40;


            //emi = cEMI.ByteArrayToStruct(arr);


            byte [] stream = KnxCon.Open("192.168.0.3");
        }

        private void Close_Click(object sender, EventArgs e)
        {
            byte[] stream = KnxCon.Close();
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
                tBResponse.AppendText(Environment.NewLine + "---  " + Text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] tele = KnxCon.GetData();
            while (tele != null)
            {
                if (tele[2] == 0x02)
                {   // Controltelegramm
                    tBResponse.AppendText(Environment.NewLine + "<C:" + KnxTools.BytesToString(tele));
                }
                else
                {   // Tunneltelegramm
                    //tBResponse.AppendText( Environment.NewLine + "<D:" + KnxTools.BytesToString(tele));
                }

                tele = KnxCon.GetData();
            }
        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI();
            emi.m_APCI = APCI_Typ.Send;
            emi.Eis1 = false;
            emi.m_source = new EIB_Adress(0);
            emi.m_destination = new EIB_Adress(2);
            emi.m_DataLen = 1;
            KnxCon.Send(emi);
        }

        private void btn_Send0_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI();
            emi.m_APCI = APCI_Typ.Send;
            emi.Eis1 = true;
            emi.m_source = new EIB_Adress(0);
            emi.m_destination = new EIB_Adress(2);
            emi.m_DataLen = 1;
            KnxCon.Send(emi);
        }

    }
}
