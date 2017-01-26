using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeData;
using EIBDef;

namespace Knx
{

    public partial class LichtControl : UserControl
    {
        public delegate void SendKnxDelegate(cEMI emi);

        private SendKnxDelegate Send;
        private EIB_Adress eibAdress_IO;
        private EIB_Adress eibAdress_Dimm;



        public LichtControl()
        {
            InitializeComponent();
        }

        public string EibAdress_Dimm
        {
            get { return eibAdress_Dimm.ToString(); }
            set { eibAdress_Dimm = new EIB_Adress(value); }
        }

        public string EibAdress_IO
        {
            get { return eibAdress_IO.ToString(); }
            set { eibAdress_IO = new EIB_Adress(value);  }
        }

        public string Titel
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }


        public void SetKnxSendFunction(SendKnxDelegate send)
        {
            this.Send = send;
        }

        private void btn_an_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_IO, true);
            Send(emi);
        }

        private void btn_aus_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_IO, false);
            Send(emi);
        }

        private void btn_heller_MouseDown(object sender, MouseEventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_Dimm, (byte)9);
            Send(emi);
        }

        private void btn_heller_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Stop_Click(null, null);
        }

        private void btn_dunkler_MouseDown(object sender, MouseEventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_Dimm, (byte)1);
            Send(emi);
        }

        private void btn_dunkler_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Stop_Click(null, null);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            cEMI emi = new cEMI(eibAdress_Dimm, (byte)0);
            Send(emi);
        }
    }
}
