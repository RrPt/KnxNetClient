using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Knx
{
    public partial class KnxNetForm : Form
    {
        KnxNetConnection KnxCon = new KnxNetConnection();
        static public System.Windows.Forms.TextBox tb_Log;

        public KnxNetForm()
        {
            InitializeComponent();
            timer1.Start();
            tb_Log = tBResponse;
        }

 
        private void GetData()
        {
            byte[] tele = KnxCon.GetData();
            if (tele != null) tBResponse.Text = tBResponse.Text + Environment.NewLine + KnxTools.BytesToString(tele);
            else tBResponse.Text = tBResponse.Text + Environment.NewLine + "no Telegramm";
        }


        // GUI

        private void Open_Click(object sender, EventArgs e)
        {
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

 
        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] tele = KnxCon.GetData();
            while (tele != null)
            {
                if (tele[2] == 0x02)
                {   // Controltelegramm
                    tBResponse.Text = tBResponse.Text + Environment.NewLine + "<C:" + KnxTools.BytesToString(tele);
                }
                else
                {   // Tunneltelegramm
                    tBResponse.Text = tBResponse.Text + Environment.NewLine + "<D:" + KnxTools.BytesToString(tele);
                }

                tele = KnxCon.GetData();
            }
        }

    }
}
