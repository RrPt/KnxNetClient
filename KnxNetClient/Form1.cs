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
                    tBResponse.AppendText( Environment.NewLine + "<D:" + KnxTools.BytesToString(tele));
                }

                tele = KnxCon.GetData();
            }
        }

    }
}
